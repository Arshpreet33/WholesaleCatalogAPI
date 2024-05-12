using MediatR;
using Persistence;
using Domain;
using AutoMapper;
using Application.Core;
using FluentValidation;

namespace Application.Categories
{
    public class Edit
    {
        public class Command : IRequest<Result<Unit>>
        {
            public Category Category { get; set; }
        }

        public class CommandValidator : AbstractValidator<Command>
        {
            public CommandValidator()
            {
                RuleFor(x => x.Category).SetValidator(new CategoryValidator());
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
                var category = await _context.Categories.FindAsync(request.Category.Id);

                if (category == null) return null;

                bool isActive = category.IsActive;
                bool isDeleted = category.IsDeleted;
                var manufacturerId = category.ManufacturerId;

                _mapper.Map(request.Category, category);

                category.IsActive = isActive;
                category.IsDeleted = isDeleted;
                category.ManufacturerId = manufacturerId;

                var result = await _context.SaveChangesAsync() > 0;

                if (!result) return Result<Unit>.Failure("Failed to update Category");

                return Result<Unit>.Success(Unit.Value);
            }
        }
    }
}