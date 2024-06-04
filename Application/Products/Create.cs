using AutoMapper;
using MediatR;
using Persistence;
using Application.Core;
using Domain;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace Application.Products
{
    public class Create
    {
        public class Command : IRequest<Result<Unit>>
        {
            public ProductDto Product { get; set; }
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
            private readonly IHostEnvironment _env;

            public Handler(DataContext context, IMapper mapper, IHostEnvironment env)
            {
                _mapper = mapper;
                _context = context;
                _env = env;
            }

            public async Task<Result<Unit>> Handle(Command request, CancellationToken cancellationToken)
            {
                var category = await _context.Categories
                    .Where(c => c.Id == request.Product.CategoryId && !c.IsDeleted)
                    .FirstOrDefaultAsync();

                if (category == null)
                {
                    return Result<Unit>.Failure("Category not found or has been deleted");
                }

                var product = _mapper.Map<Product>(request.Product);
                product.IsDeleted = false;
                product.IsActive = true;
                product.CreatedAt = DateTime.UtcNow;
                product.UpdatedAt = DateTime.UtcNow;

                _context.Products.Add(product);

                var success = await _context.SaveChangesAsync() > 0;

                if (!success) return Result<Unit>.Failure("Failed to create Product");

                // Store the Image in the File System in root of the application
                if (request.Product.Image != null)
                {
                    var fileName = product.Id.ToString() + Path.GetExtension(request.Product.Image.FileName);
                    var directoryPath = Path.Combine(_env.ContentRootPath, "images", "products");
                    Directory.CreateDirectory(directoryPath); // Create the directory if it doesn't exist
                    var filePath = Path.Combine(directoryPath, fileName);

                    using (var stream = System.IO.File.Create(filePath))
                    {
                        await request.Product.Image.CopyToAsync(stream);
                    }

                    product.ImageUrl = "/images/products/" + fileName;
                }

                return Result<Unit>.Success(Unit.Value);
            }
        }
    }
}