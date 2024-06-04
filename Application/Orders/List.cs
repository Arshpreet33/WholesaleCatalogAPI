using Application.Core;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;
using System.Linq;

namespace Application.Orders
{
    public class List
    {
        public class Query : IRequest<Result<PagedList<OrderDto>>>
        {
            public OrderParams Params { get; set; }
        }

        public class Handler : IRequestHandler<Query, Result<PagedList<OrderDto>>>
        {
            private readonly DataContext _context;
            private readonly IMapper _mapper;

            public Handler(DataContext context, IMapper mapper)
            {
                _mapper = mapper;
                _context = context;
            }
            public async Task<Result<PagedList<OrderDto>>> Handle(Query request, CancellationToken cancellationToken)
            {
                var query = _context.Orders
                  .ProjectTo<OrderDto>(_mapper.ConfigurationProvider)
                  .AsQueryable();

                query = query.Where(q => q.IsApproved == request.Params.IsApproved);

                if (request.Params.ClientId != null) // filter by Client using ClientId
                {
                    query = query.Where(q => q.Client.Id == request.Params.ClientId);
                }

                if (!string.IsNullOrEmpty(request.Params.UserName)) // filter by UserName
                {
                    var user = await _context.Users
                        .Where(u => u.UserName == request.Params.UserName && u.IsActive)
                        .FirstOrDefaultAsync();

                    if (user == null)
                    {
                        return null;
                    }

                    query = query.Where(q => q.User.UserName == user.UserName);
                }

                if (!string.IsNullOrEmpty(request.Params.OrderNumber))
                {
                    query = query.Where(q => q.OrderNumber.Contains(request.Params.OrderNumber));
                }

                return Result<PagedList<OrderDto>>.Success(
                    await PagedList<OrderDto>.CreateAsync(query, request.Params.PageNumber, request.Params.PageSize)
                );
            }
        }
    }
}
