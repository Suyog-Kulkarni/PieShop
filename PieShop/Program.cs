using BethanysPieShop.Models;
using Microsoft.EntityFrameworkCore;
using PieShop.Models;
using Microsoft.AspNetCore.Identity;
using PieShop.Areas.Identity.Data;
using PieShop.Data;
/*using PieShop.Data;
using PieShop.Areas.Identity.Data;
*/
/*using PieShop.Data;
using PieShop.Areas.Identity.Data;*/

var builder = WebApplication.CreateBuilder(args);
/*builder.Services.Configure<CookiePolicyOptions>(options =>
{
    options.MinimumSameSitePolicy = SameSiteMode.Unspecified;
    options.OnAppendCookie = cookieContext =>
        CheckSameSite(cookieContext.Context, cookieContext.CookieOptions);
    options.OnDeleteCookie = cookieContext =>
        CheckSameSite(cookieContext.Context, cookieContext.CookieOptions);
});

void CheckSameSite(HttpContext httpContext, CookieOptions options)
{
    if (options.SameSite == SameSiteMode.None)
    {
        var userAgent = httpContext.Request.Headers["User-Agent"].ToString();
        if (MyUserAgentDetectionLib.DisallowsSameSiteNone(userAgent))
        {
            options.SameSite = SameSiteMode.Unspecified;
        }
    }
}*/

// dependency injection
builder.Services.AddScoped<ICategoryRepository,CategoryRepository>();// add the interface and the class that implements
// it to the dependency injection container so that it can be used in the controllers and views of the application 
builder.Services.AddScoped<IPieRepository,PieRepository>();

/*builder.Services.AddScoped<IShoppingCart, ShoppingCart>(sp => ShoppingCart.GetCart(sp));*/// explain this code 

builder.Services.AddSession();
builder.Services.AddHttpContextAccessor();


// Add services to the container.
builder.Services.AddControllersWithViews();

//Making connection to the database 
builder.Services.AddDbContext<BethanysPieShopDbContext>(options => {
    options.UseSqlServer(builder.Configuration.GetConnectionString("BethanysPieShopContextConnection"));
});

builder.Services.AddDbContext<PieShopDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("PieShopDbContextConnection"));
});

builder.Services.AddDefaultIdentity<PieShopApplicationUser>(options => options.SignIn.RequireConfirmedAccount = false)
    .AddEntityFrameworkStores<PieShopDbContext>();

builder.Services.AddRazorPages(options =>
{
    options.Conventions.AddAreaPageRoute("Identity", "/Account/Register", "/Register");
    options.Conventions.AddAreaPageRoute("Identity", "/Account/Login", "/Login");
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseAuthentication();;
app.UseSession();
app.UseCookiePolicy();
app.UseRouting();

app.UseAuthorization();

/*app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}");

    endpoints.MapRazorPages();

});*/

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id:int?}");

app.MapRazorPages();

DbInitializer.Seed(app);

app.Run();
