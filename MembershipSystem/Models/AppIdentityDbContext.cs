namespace MembershipSystem.Models
{
    public class AppIdentityDbContext : IdentityDbContext<AppUser, AppRole, int>
    {
        public AppIdentityDbContext(DbContextOptions options): base(options)
        {
        }

        public AppIdentityDbContext(DbContextOptions<IdentityDbContext> options) : base(options)
        {
        }
    }
}