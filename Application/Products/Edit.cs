using MediatR;
using Persistence;
using Domain;
using AutoMapper;
using Application.Core;
using FluentValidation;

namespace Application.Products
{
    public class Edit
    {
        public class Command : IRequest<Result<Unit>>
        {
            public Product Product { get; set; }
        }

        public class CommandValidator : AbstractValidator<Command>
        {
            public CommandValidator()
            {
                RuleFor(x => x.Product).SetValidator(new ProductValidator());
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
                var product = await _context.Products.FindAsync(request.Product.Id);

                if (product == null) return null;

                bool isActive = product.IsActive;
                bool isDeleted = product.IsDeleted;
                var categoryId = product.CategoryId;
                var createdAt = product.CreatedAt;

                _mapper.Map(request.Product, product);

                product.IsActive = isActive;
                product.IsDeleted = isDeleted;
                product.CategoryId = categoryId;
                product.CreatedAt = createdAt;
                product.UpdatedAt = DateTime.UtcNow;

                var result = await _context.SaveChangesAsync() > 0;

                if (!result) return Result<Unit>.Failure("Failed to update Product");

                return Result<Unit>.Success(Unit.Value);
            }
        }
    }
}