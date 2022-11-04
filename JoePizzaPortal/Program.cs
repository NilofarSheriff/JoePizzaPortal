using Microsoft.EntityFrameworkCore;
using JoePizzaPortal.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Session;


string cs = "server=LAPTOP-KUI0108O;database=Joe_Pizza_Portal;trusted_connection=true";

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<Joe_Pizza_PortalContext>(options => options.UseSqlServer(cs));

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDistributedMemoryCache();

builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromSeconds(10);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
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


app.UseRouting();
app.UseSession();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
