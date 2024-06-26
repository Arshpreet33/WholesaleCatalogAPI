﻿using Microsoft.EntityFrameworkCore;
using Persistence;
using Domain;
using MediatR;
using Application.Core;

namespace Application.Categories
{
    public class Delete
    {
        public class Command : IRequest<Result<Unit>>
        {
            public Guid Id { get; set; }
        }

        public class Handler : IRequestHandler<Command, Result<Unit>>
        {
            private readonly DataContext _context;

            public Handler(DataContext context)
            {
                _context = context;
            }

            public async Task<Result<Unit>> Handle(Command request, CancellationToken cancellationToken)
            {
                var category = await _context.Categories.FindAsync(request.Id);

                if (category == null) return null;

                category.IsDeleted = true;
                category.DeletedAt = DateTime.UtcNow;

                var products = await _context.Products.Where(p => p.CategoryId == category.Id).ToListAsync();

                foreach (var product in products)
                {
                    product.IsDeleted = true;
                    product.DeletedAt = DateTime.UtcNow;
                }

                var result = await _context.SaveChangesAsync() > 0;

                if (!result) return Result<Unit>.Failure("Failed to delete Category");

                return Result<Unit>.Success(Unit.Value);
            }
        }
    }
}
