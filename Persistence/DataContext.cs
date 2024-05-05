using Domain;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Persistence
{
    public class DataContext : IdentityDbContext<AppUser>
    {
        public DataContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<Manufacturer> Manufacturers { get; set; }
        public DbSet<Category> Categories { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Category>()
                 .HasOne(m => m.Manufacturer)
                 .WithMany(c => c.Categories)
                 .HasForeignKey(m => m.ManufacturerId)
                 .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
