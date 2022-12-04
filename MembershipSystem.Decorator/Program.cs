using MembershipSystem.Decorator.Repositories;
using MembershipSystem.Decorator.Repositories.Decorators;
using Microsoft.Extensions.Caching.Memory;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppIdentityDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

builder.Services.AddIdentity<AppUser, IdentityRole>(options =>
{
    options.User.RequireUniqueEmail = true;
}).AddEntityFrameworkStores<AppIdentityDbContext>();

builder.Services.AddControllersWithViews().AddRazorRuntimeCompilation();
builder.Services.AddMemoryCache();
builder.Services.AddScoped<IProductRepository>(provider =>
{
    var context = provider.GetRequiredService<AppIdentityDbContext>();
    var memoryCache = provider.GetRequiredService<IMemoryCache>();
    var productRepository = new ProductRepository(context);

    return new ProductRepositoryCacheDecorator(productRepository, memoryCache);
});

SeedData.AddSeedData(builder);

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.Use(async (a, b) =>
{
    try
    {
        await b();
    }
    catch (Exception ex)
    {

    }
});

app.UseRouting();

app.UseAuthentication(); //Kimlik Doðrulama
app.UseAuthorization(); //Yetkilendirme

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
