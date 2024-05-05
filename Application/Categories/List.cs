using Application.Core;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Persistence;

namespace Application.Categories
{
    public class List
    {
        public class Query : IRequest<Result<PagedList<CategoryDto>>>
        {
            public CategoryParams Params { get; set; }
        }

        public class Handler : IRequestHandler<Query, Result<PagedList<CategoryDto>>>
        {
            private readonly DataContext _context;
            private readonly IMapper _mapper;

            public Handler(DataContext context, IMapper mapper)
            {
                _mapper = mapper;
                _context = context;
            }
            public async Task<Result<PagedList<CategoryDto>>> Handle(Query request, CancellationToken cancellationToken)
            {
                var query = _context.Categories.Where(m => !m.IsDeleted)   // do not return the deleted Categories
                  .ProjectTo<CategoryDto>(_mapper.ConfigurationProvider)
                  .AsQueryable();

                query = query.Where(q => q.IsActive == request.Params.IsActive); // filter by active Categories

                if (!string.IsNullOrEmpty(request.Params.Name)) // filter by name using search string 'Name
                {
                    query = query.Where(q => q.Name.Contains(request.Params.Name));
                }

                return Result<PagedList<CategoryDto>>.Success(
                    await PagedList<CategoryDto>.CreateAsync(query, request.Params.PageNumber, request.Params.PageSize)
                );
            }
        }
    }
}