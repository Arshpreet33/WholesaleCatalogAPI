using MediatR;
using Persistence;
using Domain;
using AutoMapper;
using Application.Core;
using FluentValidation;

namespace Application.Clients
{
    public class Edit
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
                _context = context;
                _mapper = mapper;
            }

            public async Task<Result<Unit>> Handle(Command request, CancellationToken cancellationToken)
            {
                var client = await _context.Clients.FindAsync(request.Client.Id);

                if (client == null) return null;

                bool isActive = client.IsActive;
                bool isDeleted = client.IsDeleted;
                var cretedAt = client.CreatedAt;
                var code = client.Code;

                _mapper.Map(request.Client, client);

                client.Code = code;
                client.IsActive = isActive;
                client.IsDeleted = isDeleted;
                client.CreatedAt = cretedAt;
                client.UpdatedAt = DateTime.UtcNow;

                var result = await _context.SaveChangesAsync() > 0;

                if (!result) return Result<Unit>.Failure("Failed to update Client");

                return Result<Unit>.Success(Unit.Value);
            }
        }
    }
}