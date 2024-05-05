using AutoMapper;
using MediatR;
using Persistence;
using Application.Core;
using Domain;
using FluentValidation;

namespace Application.Categories
{
    public class Create
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
                _mapper = mapper;
                _context = context;
            }

            public async Task<Result<Unit>> Handle(Command request, CancellationToken cancellationToken)
            {
                var category = _mapper.Map<Category>(request.Category);
                category.IsDeleted = false;
                category.IsActive = true;

                _context.Categories.Add(category);

                var success = await _context.SaveChangesAsync() > 0;

                if (!success) return Result<Unit>.Failure("Failed to create Category");

                return Result<Unit>.Success(Unit.Value);
            }
        }
    }
}
