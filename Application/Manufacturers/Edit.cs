using MediatR;
using Persistence;
using Domain;
using AutoMapper;
using Application.Core;
using FluentValidation;

namespace Application.Manufacturers
{
    public class Edit
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
                _context = context;
                _mapper = mapper;
            }

            public async Task<Result<Unit>> Handle(Command request, CancellationToken cancellationToken)
            {
                var manufacturer = await _context.Manufacturers.FindAsync(request.Manufacturer.Id);

                if (manufacturer == null) return null;

                bool isActive = manufacturer.IsActive;
                bool isDeleted = manufacturer.IsDeleted;
                var createdAt = manufacturer.CreatedAt;

                _mapper.Map(request.Manufacturer, manufacturer);

                manufacturer.IsActive = isActive;
                manufacturer.IsDeleted = isDeleted;
                manufacturer.CreatedAt = createdAt;
                manufacturer.UpdatedAt = DateTime.UtcNow;

                var result = await _context.SaveChangesAsync() > 0;

                if (!result) return Result<Unit>.Failure("Failed to update Manufacturer");

                return Result<Unit>.Success(Unit.Value);
            }
        }
    }
}