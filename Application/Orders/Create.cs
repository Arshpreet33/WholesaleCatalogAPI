using AutoMapper;
using MediatR;
using Persistence;
using Application.Core;
using Domain;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Application.Clients;

namespace Application.Orders
{
    public class Create
    {
        public class Command : IRequest<Result<Unit>>
        {
            public Order Order { get; set; }
        }

        public class CommandValidator : AbstractValidator<Command>
        {
            public CommandValidator()
            {
                RuleFor(x => x.Order).SetValidator(new OrderValidator());
            }
        }

        public class Handler : IRequestHandler<Command, Result<Unit>>
        {
            private readonly DataContext _context;
            private readonly IMapper _mapper;

            public Handler(DataContext context, IMapper mapper)
            {
                _mapper = mapper;
                _context = context;
            }

            public async Task<Result<Unit>> Handle(Command request, CancellationToken cancellationToken)
            {
                var client = await _context.Clients
                    .Where(c => c.Id == request.Order.ClientId && !c.IsDeleted && c.IsActive)
                    .FirstOrDefaultAsync();

                if (client == null)
                {
                    return Result<Unit>.Failure("Client not found or has been deleted or inactive");
                }

                var user = await _context.Users
                    .Where(u => u.UserName == request.Order.UserName && u.IsActive)
                    .FirstOrDefaultAsync();

                if (user == null)
                {
                    return Result<Unit>.Failure("User not found or revoked");
                }

                request.Order.Client = null;
                request.Order.User = null;

                var order = _mapper.Map<Order>(request.Order);
                order.UserId = user.Id;
                order.ClientId = client.Id;
                order.UserName = user.UserName;
                order.IsApproved = false;
                order.IsDeleted = false;
                order.IsActive = true;
                order.OrderDate = DateTime.UtcNow;
                order.CreatedAt = DateTime.UtcNow;
                order.UpdatedAt = DateTime.UtcNow;
                order.CreatedBy = user.UserName;
                order.UpdatedBy = user.UserName;
                order.OrderItems = null;

                // Count the number of orders for the user
                var orderCount = await _context.Orders
                    .Where(o => o.User.UserName == user.UserName)
                    .CountAsync();

                // Generate a unique OrderNumber
                order.OrderNumber = "O-" + user.UserName + "-" + client.Code + "-" + (orderCount + 1).ToString("D5");

                _context.Orders.Add(order);

                var success = await _context.SaveChangesAsync() > 0;

                if (!success) return Result<Unit>.Failure("Failed to create Order");

                var orderItems = _mapper.Map<List<OrderItem>>(request.Order.OrderItems);
                foreach (var item in orderItems)
                {
                    var product = await _context.Products
                        .Where(p => p.Id == item.ProductId && !p.IsDeleted && p.IsActive)
                        .FirstOrDefaultAsync();

                    if (product == null)
                    {
                        return Result<Unit>.Failure("Product not found or has been deleted or inactive");
                    }

                    item.Product = null;

                    item.OrderId = order.Id;
                    item.ProductId = product.Id;
                    item.IsActive = true;
                    item.IsDeleted = false;
                    item.CreatedAt = DateTime.UtcNow;
                    item.UpdatedAt = DateTime.UtcNow;
                    _context.OrderItems.Add(item);
                }

                success = await _context.SaveChangesAsync() > 0;

                if (!success) return Result<Unit>.Failure("Failed to create Order");

                return Result<Unit>.Success(Unit.Value);
            }
        }
    }
}
