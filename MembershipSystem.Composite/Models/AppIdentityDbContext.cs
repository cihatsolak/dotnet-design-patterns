using MembershipSystem.Composite.Models;
using Microsoft.EntityFrameworkCore.Design;

namespace MembershipSystem.Models
{
    public class AppIdentityDbContext : IdentityDbContext<AppUser>
    {
        public AppIdentityDbContext(DbContextOptions<AppIdentityDbContext> options) : base(options)
        {
        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Book> Books { get; set; }

        public class AppIdentityDbContextFactory : IDesignTimeDbContextFactory<AppIdentityDbContext>
        {
            public AppIdentityDbContext CreateDbContext(string[] args)
            {
                var optionsBuilder = new DbContextOptionsBuilder<AppIdentityDbContext>();
                optionsBuilder.UseSqlServer("Server=localhost,1433;Database=DesignPattern;User=sa;Password=Password123");

                return new AppIdentityDbContext(optionsBuilder.Options);
            }
        }
    }
}