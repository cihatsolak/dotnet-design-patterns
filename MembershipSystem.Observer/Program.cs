var builder = WebApplication.CreateBuilder(args);

//builder.Services.AddSingleton<IUserObserverSubject>(sp =>
//{
//    UserObserverSubject userObserverSubject = new();

//    userObserverSubject.RegisterObserver(new UserObserverWriteToConsole(sp));
//    userObserverSubject.RegisterObserver(new UserObserverCreateDiscount(sp));
//    userObserverSubject.RegisterObserver(new UserObserverSendEmail(sp));

//    return userObserverSubject;
//});

builder.Services.AddMediatR(Assembly.GetExecutingAssembly());


builder.Services.AddDbContext<AppIdentityDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

builder.Services.AddIdentity<AppUser, IdentityRole>(options =>
{
    options.User.RequireUniqueEmail = true;
}).AddEntityFrameworkStores<AppIdentityDbContext>();

builder.Services.AddControllersWithViews().AddRazorRuntimeCompilation();

SeedData.AddSeedData(builder);

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication(); //Kimlik Doðrulama
app.UseAuthorization(); //Yetkilendirme

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
