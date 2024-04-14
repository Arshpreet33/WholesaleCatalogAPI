using Application.Core;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Persistence;

namespace Application.Clients
{
    public class List
    {
        public class Query : IRequest<Result<PagedList<ClientDto>>>
        {
            public ClientParams Params { get; set; }
        }

        public class Handler : IRequestHandler<Query, Result<PagedList<ClientDto>>>
        {
            private readonly DataContext _context;
            private readonly IMapper _mapper;

            public Handler(DataContext context, IMapper mapper)
            {
                _mapper = mapper;
                _context = context;
            }
            public async Task<Result<PagedList<ClientDto>>> Handle(Query request, CancellationToken cancellationToken)
            {
                var query = _context.Clients.Where(c => !c.IsDeleted)   // do not return the deleted clients
                  .ProjectTo<ClientDto>(_mapper.ConfigurationProvider)
                  .AsQueryable();

                query = query.Where(q => q.IsActive == request.Params.IsActive); // filter by active clients

                if (!string.IsNullOrEmpty(request.Params.Name)) // filter by name, code or email using search string 'Name
                {
                    query = query.Where(
                        q => q.Name.Contains(request.Params.Name) ||
                            q.Code.Contains(request.Params.Name) ||
                            q.Email.Contains(request.Params.Name)
                    );
                }

                return Result<PagedList<ClientDto>>.Success(
                  await PagedList<ClientDto>.CreateAsync(query, request.Params.PageNumber, request.Params.PageSize)
                );
            }
        }
    }
}