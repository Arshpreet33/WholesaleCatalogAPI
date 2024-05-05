using Microsoft.EntityFrameworkCore;
using Persistence;
using Domain;
using MediatR;
using Application.Core;

namespace Application.Categories
{
    public class ToggleActive
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
                var category = await _context.Categories.FindAsync(request.Id);

                if (category == null) return null;

                category.IsActive = !category.IsActive;

                var result = await _context.SaveChangesAsync() > 0;

                if (!result) return Result<Unit>.Failure("Failed to Activate/deactivate Category");

                return Result<Unit>.Success(Unit.Value);
            }
        }
    }
}
