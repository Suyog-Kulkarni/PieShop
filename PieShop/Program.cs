using Microsoft.EntityFrameworkCore;
using PieShop.Models;

var builder = WebApplication.CreateBuilder(args);

// dependency injection
builder.Services.AddScoped<ICategoryRepository,CategoryRepository>();// add the interface and the class that implements
// it to the dependency injection container so that it can be used in the controllers and views of the application 
builder.Services.AddScoped<IPieRepository,PieRepository>();


// Add services to the container.
builder.Services.AddControllersWithViews();

//Making connection to the database 
builder.Services.AddDbContextPool<BethanysPieShopDbContext>(options => {
    options.UseSqlServer(builder.Configuration.GetConnectionString("BethanysPieShopContextConnection"));
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
