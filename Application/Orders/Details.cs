using Application.Core;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Orders
{
    public class Details
    {
        public class Query : IRequest<Result<OrderDto>>
        {
            public Guid Id { get; set; }
        }

        public class Handler : IRequestHandler<Query, Result<OrderDto>>
        {
            private readonly DataContext _context;
            private readonly IMapper _mapper;

            public Handler(DataContext context, IMapper mapper)
            {
                _mapper = mapper;
                _context = context;
            }

            public async Task<Result<OrderDto>> Handle(Query request, CancellationToken cancellationToken)
            {
                var order = await _context.Orders
                  .ProjectTo<OrderDto>(_mapper.ConfigurationProvider)
                  .FirstOrDefaultAsync(x => x.Id == request.Id);

                return Result<OrderDto>.Success(order);
            }
        }
    }
}
