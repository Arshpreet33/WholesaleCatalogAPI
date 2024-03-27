using Application.Core;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Products
{
    public class Details
    {
        public class Query : IRequest<Result<ProductDto>>
        {
            public Guid Id { get; set; }
        }

        public class Handler : IRequestHandler<Query, Result<ProductDto>>
        {
            private readonly DataContext _context;
            private readonly IMapper _mapper;

            public Handler(DataContext context, IMapper mapper)
            {
                _mapper = mapper;
                _context = context;

            }
            public async Task<Result<ProductDto>> Handle(Query request, CancellationToken cancellationToken)
            {
                var activity = await _context.Products
                  .ProjectTo<ProductDto>(_mapper.ConfigurationProvider)
                  .FirstOrDefaultAsync(x => x.Id == request.Id);

                return Result<ProductDto>.Success(activity);
            }
        }
    }
}