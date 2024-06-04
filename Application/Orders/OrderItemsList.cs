using Application.Core;
using Application.Orders;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Persistence;
using System.Linq;

namespace Application.Orders
{
    public class OrderItemsList
    {
        public class Query : IRequest<Result<PagedList<OrderItemDto>>>
        {
            public OrderItemParams Params { get; set; }
        }

        public class Handler : IRequestHandler<Query, Result<PagedList<OrderItemDto>>>
        {
            private readonly DataContext _context;
            private readonly IMapper _mapper;

            public Handler(DataContext context, IMapper mapper)
            {
                _mapper = mapper;
                _context = context;
            }
            public async Task<Result<PagedList<OrderItemDto>>> Handle(Query request, CancellationToken cancellationToken)
            {
                var query = _context.OrderItems
                  .ProjectTo<OrderItemDto>(_mapper.ConfigurationProvider)
                  .AsQueryable();

                // filter by Order using OrderId
                query = query.Where(q => q.Order.Id == request.Params.OrderId);

                return Result<PagedList<OrderItemDto>>.Success(
                    await PagedList<OrderItemDto>.CreateAsync(query, request.Params.PageNumber, request.Params.PageSize)
                );
            }
        }
    }
}
