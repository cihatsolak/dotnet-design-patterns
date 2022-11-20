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
                userManager.CreateAsync(new AppUser
                {
                    UserName = "user1",
                    Email = "user1@hotmail.com",
                    PictureUrl = "/userpictures/primeuserpicture.png",
                    Description = "Lorem Ipsum is simply dummy text of the printing and typesetting industry."
                }, "Password12*").Wait();

                userManager.CreateAsync(new AppUser
                {
                    UserName = "user2",
                    Email = "user2@hotmail.com",
                    PictureUrl = "/userpictures/primeuserpicture.png",
                    Description = "Lorem Ipsum is simply dummy text of the printing and typesetting industry."
                }, "Password12*").Wait();

                userManager.CreateAsync(new AppUser
                {
                    UserName = "user3",
                    Email = "user3@hotmail.com",
                    PictureUrl = "/userpictures/primeuserpicture.png",
                    Description = "Lorem Ipsum is simply dummy text of the printing and typesetting industry."
                }, "Password12*").Wait();

                userManager.CreateAsync(new AppUser
                {
                    UserName = "user4",
                    Email = "user4@hotmail.com",
                    PictureUrl = "/userpictures/primeuserpicture.png",
                    Description = "Lorem Ipsum is simply dummy text of the printing and typesetting industry."
                }, "Password12*").Wait();

                userManager.CreateAsync(new AppUser
                {
                    UserName = "user5",
                    Email = "user5@hotmail.com",
                    PictureUrl = "/userpictures/primeuserpicture.png",
                    Description = "Lorem Ipsum is simply dummy text of the printing and typesetting industry."
                }, "Password12*").Wait();
            }
        }
    }
}
