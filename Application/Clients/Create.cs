using AutoMapper;
using MediatR;
using Persistence;
using Application.Core;
using Domain;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace Application.Clients
{
    public class Create
    {
        public class Command : IRequest<Result<Unit>>
        {
            public Client Client { get; set; }
        }

        public class CommandValidator : AbstractValidator<Command>
        {
            public CommandValidator()
            {
                RuleFor(x => x.Client).SetValidator(new ClientValidator());
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
                // Check if a client with the same ClientCode already exists
                var existingClient = await _context.Clients
                    .Where(c => c.Code == request.Client.Code)
                    .FirstOrDefaultAsync();

                if (existingClient != null)
                {
                    return Result<Unit>.Failure("Client Code is not unique");
                }

                var client = _mapper.Map<Client>(request.Client);
                client.IsDeleted = false;
                client.IsActive = true;
                client.CreatedAt = DateTime.UtcNow;
                client.UpdatedAt = DateTime.UtcNow;

                _context.Clients.Add(client);

                var success = await _context.SaveChangesAsync() > 0;

                if (!success) return Result<Unit>.Failure("Failed to create client");

                return Result<Unit>.Success(Unit.Value);
            }
        }
    }
}
