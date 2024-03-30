using AutoMapper;
using MediatR;
using Persistence;
using Application.Core;
using Domain;
using FluentValidation;

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
                var client = _mapper.Map<Client>(request.Client);
                client.IsDeleted = false;
                client.IsActive = true;

                _context.Clients.Add(client);

                var success = await _context.SaveChangesAsync() > 0;

                if (!success) return Result<Unit>.Failure("Failed to create client");

                return Result<Unit>.Success(Unit.Value);
            }
        }
    }
}
