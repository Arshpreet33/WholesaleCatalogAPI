using Microsoft.EntityFrameworkCore;
using Persistence;
using Domain;
using MediatR;
using Application.Core;

namespace Application.Clients
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
                var client = await _context.Clients.FindAsync(request.Id);

                if (client == null) return null;

                client.IsDeleted = true;
                client.DeletedAt = DateTime.UtcNow;

                var result = await _context.SaveChangesAsync() > 0;

                if (!result) return Result<Unit>.Failure("Failed to delete Client");

                return Result<Unit>.Success(Unit.Value);
            }
        }
    }
}
