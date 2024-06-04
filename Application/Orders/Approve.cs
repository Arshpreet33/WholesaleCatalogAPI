using Microsoft.EntityFrameworkCore;
using Persistence;
using Domain;
using MediatR;
using Application.Core;

namespace Application.Orders
{
    public class Approve
    {
        public class Command : IRequest<Result<Unit>>
        {
            public Guid Id { get; set; }
        }

        public class Handler : IRequestHandler<Command, Result<Unit>>
        {
            private readonly DataContext _context;

            public Handler(DataContext context)
            {
                _context = context;
            }

            public async Task<Result<Unit>> Handle(Command request, CancellationToken cancellationToken)
            {
                var order = await _context.Orders.FindAsync(request.Id);

                if (order == null) return null;

                order.IsApproved = true;
                order.UpdatedAt = DateTime.UtcNow;
                order.ApprovedAt = DateTime.UtcNow;

                var result = await _context.SaveChangesAsync() > 0;

                if (!result) return Result<Unit>.Failure("Failed to approve Order");

                return Result<Unit>.Success(Unit.Value);
            }
        }
    }
}
