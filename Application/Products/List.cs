using Application.Core;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Persistence;
using System.Linq;

namespace Application.Products
{
    public class List
    {
        public class Query : IRequest<Result<PagedList<ProductDto>>>
        {
            public ProductParams Params { get; set; }
        }

        public class Handler : IRequestHandler<Query, Result<PagedList<ProductDto>>>
        {
            private readonly DataContext _context;
            private readonly IMapper _mapper;

            public Handler(DataContext context, IMapper mapper)
            {
                _mapper = mapper;
                _context = context;
            }
            public async Task<Result<PagedList<ProductDto>>> Handle(Query request, CancellationToken cancellationToken)
            {
                var query = _context.Products.Where(p => !p.IsDeleted)   // do not return the deleted Products
                  .ProjectTo<ProductDto>(_mapper.ConfigurationProvider)
                  .AsQueryable();

                query = query.Where(q => q.IsActive == request.Params.IsActive); // filter by active Products

                if(request.Params.ManufacturerId != null) // filter by Manufacturer using ManufacturerId
                {
                    query = query.Where(q => q.Category.Manufacturer.Id == request.Params.ManufacturerId);
                }

                if (request.Params.CategoryId != null) // filter by Category using CategoryId
                {
                    query = query.Where(q => q.Category.Id == request.Params.CategoryId);
                }

                if (!string.IsNullOrEmpty(request.Params.Name)) // filter by name using search string 'Name
                {
                    query = query.Where(
                        q => q.Name.Contains(request.Params.Name) ||
                            q.Code.Contains(request.Params.Name)
                    );
                }

                return Result<PagedList<ProductDto>>.Success(
                    await PagedList<ProductDto>.CreateAsync(query, request.Params.PageNumber, request.Params.PageSize)
                );
            }
        }
    }
}