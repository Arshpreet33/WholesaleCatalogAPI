using AutoMapper;
using MediatR;
using Persistence;
using Application.Core;
using Domain;
using FluentValidation;

namespace Application.Manufacturers
{
    public class Create
    {
        public class Command : IRequest<Result<Unit>>
        {
            public Manufacturer Manufacturer { get; set; }
        }

        public class CommandValidator : AbstractValidator<Command>
        {
            public CommandValidator()
            {
                RuleFor(x => x.Manufacturer).SetValidator(new ManufacturerValidator());
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
                var manufacturer = _mapper.Map<Manufacturer>(request.Manufacturer);
                manufacturer.IsDeleted = false;
                manufacturer.IsActive = true;

                _context.Manufacturers.Add(manufacturer);

                var success = await _context.SaveChangesAsync() > 0;

                if (!success) return Result<Unit>.Failure("Failed to create manufacturer");

                return Result<Unit>.Success(Unit.Value);
            }
        }
    }
}
