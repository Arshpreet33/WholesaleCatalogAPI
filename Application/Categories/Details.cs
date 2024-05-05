using Application.Core;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Categories
{
    public class Details
    {
        public class Query : IRequest<Result<CategoryDto>>
        {
            public Guid Id { get; set; }
        }

        public class Handler : IRequestHandler<Query, Result<CategoryDto>>
        {
            private readonly DataContext _context;
            private readonly IMapper _mapper;

            public Handler(DataContext context, IMapper mapper)
            {
                _mapper = mapper;
                _context = context;

            }
            public async Task<Result<CategoryDto>> Handle(Query request, CancellationToken cancellationToken)
            {
                var category = await _context.Categories.Where(m => !m.IsDeleted)   // do not return the deleted Categories
                  .ProjectTo<CategoryDto>(_mapper.ConfigurationProvider)
                  .FirstOrDefaultAsync(x => x.Id == request.Id);

                return Result<CategoryDto>.Success(category);
            }
        }
    }
}