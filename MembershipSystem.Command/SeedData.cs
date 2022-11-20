namespace MembershipSystem
{
    public static class SeedData
    {
        public static async void AddSeedData(this WebApplicationBuilder builder)
        {
            var serviceProvider = builder.Services.BuildServiceProvider();
            var identityDbContext = serviceProvider.GetRequiredService<AppIdentityDbContext>();
            var userManager = serviceProvider.GetRequiredService<UserManager<AppUser>>();

            identityDbContext.Database.Migrate();

            if (!userManager.Users.Any())
            {
                userManager.CreateAsync(new AppUser
                {
                    UserName = "user1",
                    Email = "user1@hotmail.com",
                }, "Password12*").Wait();

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

                Enumerable.Range(1, 30).ToList().ForEach(x =>
                {
                    identityDbContext.Products.Add(new Product() { Name = $"kalem {x}", Price = x * 100, Stock = x + 50 });
                });

                identityDbContext.SaveChangesAsync().Wait();
            }
        }
    }
}
