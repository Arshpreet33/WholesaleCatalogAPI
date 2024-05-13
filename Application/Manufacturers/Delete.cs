using Microsoft.EntityFrameworkCore;
using Persistence;
using Domain;
using MediatR;
using Application.Core;

namespace Application.Manufacturers
{
    public class Delete
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
                var manufacturer = await _context.Manufacturers.FindAsync(request.Id);

                if (manufacturer == null) return null;

                manufacturer.IsDeleted = true;
                manufacturer.DeletedAt = DateTime.UtcNow;

                var categories = await _context.Categories.Where(c => c.ManufacturerId == manufacturer.Id).ToListAsync();

                foreach (var category in categories)
                {
                    category.IsDeleted = true;
                    category.DeletedAt = DateTime.UtcNow;
                }

                var result = await _context.SaveChangesAsync() > 0;

                if (!result) return Result<Unit>.Failure("Failed to delete Manufacturer");

                return Result<Unit>.Success(Unit.Value);
            }
        }
    }
}
