using Application.Core;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Persistence;

namespace Application.Manufacturers
{
    public class List
    {
        public class Query : IRequest<Result<PagedList<ManufacturerDto>>>
        {
            public ManufacturerParams Params { get; set; }
        }

        public class Handler : IRequestHandler<Query, Result<PagedList<ManufacturerDto>>>
        {
            private readonly DataContext _context;
            private readonly IMapper _mapper;

            public Handler(DataContext context, IMapper mapper)
            {
                _mapper = mapper;
                _context = context;
            }
            public async Task<Result<PagedList<ManufacturerDto>>> Handle(Query request, CancellationToken cancellationToken)
            {
                var query = _context.Manufacturers.Where(m => !m.IsDeleted)   // do not return the deleted manufacturers
                  .ProjectTo<ManufacturerDto>(_mapper.ConfigurationProvider)
                  .AsQueryable();

                query = query.Where(q => q.IsActive == request.Params.IsActive); // filter by active manufacturers

                if (!string.IsNullOrEmpty(request.Params.Name)) // filter by name using search string 'Name
                {
                    query = query.Where(q => q.Name.Contains(request.Params.Name));
                }

                return Result<PagedList<ManufacturerDto>>.Success(
                    await PagedList<ManufacturerDto>.CreateAsync(query, request.Params.PageNumber, request.Params.PageSize)
                                                    );
            }
        }
    }
}