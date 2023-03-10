using Microsoft.EntityFrameworkCore;
using prjhouse.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDistributedMemoryCache();             //µù¥Usession
builder.Services.AddSession(op =>                         //µù¥Usession
{
    op.IOTimeout = TimeSpan.FromMinutes(20);
    op.Cookie.HttpOnly = true;
    op.Cookie.IsEssential = true;
});

builder.Services.AddDbContext<HouseContext>(               
 options => options.UseSqlServer(
 builder.Configuration.GetConnectionString("HouseConnection")
));

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
app.UseSession();                                              //±Ò¥Îsession
app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
