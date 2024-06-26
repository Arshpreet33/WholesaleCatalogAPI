﻿using MediatR;
using Persistence;
using Domain;
using AutoMapper;
using Application.Core;
using FluentValidation;
using Microsoft.Extensions.Hosting;

namespace Application.Products
{
    public class Edit
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

                    product.ImageUrl = "/api/images/products/" + fileName;
                }

                return Result<Unit>.Success(Unit.Value);
            }
        }
    }
}