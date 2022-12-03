using MembershipSystem.Composite.Models;

namespace MembershipSystem
{
    public static class SeedData
    {
        public static void AddSeedData(this WebApplicationBuilder builder)
        {
            var serviceProvider = builder.Services.BuildServiceProvider();
            var identityDbContext = serviceProvider.GetRequiredService<AppIdentityDbContext>();
            var userManager = serviceProvider.GetRequiredService<UserManager<AppUser>>();

            identityDbContext.Database.Migrate();

            if (!userManager.Users.Any())
            {
                var user1 = new AppUser
                {
                    UserName = "user1",
                    Email = "user1@hotmail.com",
                };

                userManager.CreateAsync(user1, "Password12*").Wait();

                userManager.CreateAsync(new AppUser
                {
                    UserName = "user2",
                    Email = "user2@hotmail.com",
                }, "Password12*").Wait();

                userManager.CreateAsync(new AppUser
                {
                    UserName = "user3",
                    Email = "user3@hotmail.com",
                }, "Password12*").Wait();

                userManager.CreateAsync(new AppUser
                {
                    UserName = "user4",
                    Email = "user4@hotmail.com",
                }, "Password12*").Wait();

                userManager.CreateAsync(new AppUser
                {
                    UserName = "user5",
                    Email = "user5@hotmail.com",
                }, "Password12*").Wait();

                var newCategory1 = new Category { Name = "Suç romanları", ReferenceId = 0, UserId = user1.Id };
                var newCategory2 = new Category { Name = "Cinayet romanları", ReferenceId = 0, UserId = user1.Id };
                var newCategory3 = new Category { Name = "Polisiye romanları", ReferenceId = 0, UserId = user1.Id };

                identityDbContext.Categories.AddRange(newCategory1, newCategory2, newCategory3);
                identityDbContext.SaveChanges();

                var subCategory1 = new Category { Name = "Suç romanları 1", ReferenceId = newCategory1.Id, UserId = user1.Id };
                var subCategory2 = new Category { Name = "Cinayet romanları 1", ReferenceId = newCategory2.Id, UserId = user1.Id };
                var subCategory3 = new Category { Name = "Polisiye romanları 1", ReferenceId = newCategory3.Id, UserId = user1.Id };

                identityDbContext.Categories.AddRange(subCategory1, subCategory2, subCategory3);
                identityDbContext.SaveChanges();

                var subCategory4 = new Category { Name = "Cinayet romanları 1.1", ReferenceId = subCategory2.Id, UserId = user1.Id };

                identityDbContext.Categories.Add(subCategory4);
                identityDbContext.SaveChanges();
            }
        }
    }
}
