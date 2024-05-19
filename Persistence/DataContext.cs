using Domain;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;

namespace Persistence
{
    public class DataContext : IdentityDbContext<AppUser>
    {
        public DataContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Client> Clients { get; set; }
        public DbSet<Manufacturer> Manufacturers { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Category>()
                 .HasOne(m => m.Manufacturer)
                 .WithMany(c => c.Categories)
                 .HasForeignKey(m => m.ManufacturerId)
                 .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<Product>()
                .HasOne(c => c.Category)
                .WithMany(p => p.Products)
                .HasForeignKey(c => c.CategoryId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<Product>()
                .Property(p => p.UnitPrice)
                .HasColumnType("decimal(18, 2)")
                .HasPrecision(18, 2);

            builder.Entity<Product>()
                .Property(p => p.CasePrice)
                .HasColumnType("decimal(18, 2)")
                .HasPrecision(18, 2);
        }
    }
}
