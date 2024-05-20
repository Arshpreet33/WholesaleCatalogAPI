using Application.Core;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Persistence;

namespace Application.Users
{
    public class List
    {
        public class Query : IRequest<Result<PagedList<UserDto>>>
        {
            public UserParams Params { get; set; }
        }

        public class Handler : IRequestHandler<Query, Result<PagedList<UserDto>>>
        {
            private readonly DataContext _context;
            private readonly IMapper _mapper;

            public Handler(DataContext context, IMapper mapper)
            {
                _mapper = mapper;
                _context = context;
            }

            public async Task<Result<PagedList<UserDto>>> Handle(Query request, CancellationToken cancellationToken)
            {
                var query = _context.Users.Where(u => u.Role == "User") // filter by role 'User'
                  .ProjectTo<UserDto>(_mapper.ConfigurationProvider)
                  .AsQueryable();

                query = query.Where(q => q.IsActive == request.Params.IsActive); // filter by active clients

                if (!string.IsNullOrEmpty(request.Params.Name)) // filter by name, code or email using search string 'Name'
                {
                    query = query.Where(
                        q => q.DisplayName.Contains(request.Params.Name) ||
                            q.UserName.Contains(request.Params.Name) ||
                            q.Email.Contains(request.Params.Name)
                    );
                }

                return Result<PagedList<UserDto>>.Success(
                  await PagedList<UserDto>.CreateAsync(query, request.Params.PageNumber, request.Params.PageSize)
                );
            }
        }
    }
}
