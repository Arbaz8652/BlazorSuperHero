global using BlazorSuperHero.Shared;
global using Microsoft.EntityFrameworkCore;
global using BlazorSuperHero.Server.Data;
using Microsoft.AspNetCore.ResponseCompression;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();
builder.Services.AddDbContext<DataContext>(options=>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DeafaulConnection")));

/*
string connectionString = Configuration.GetConnectionString("DefaultConnection");
services.AddDbContextPool<AppDbContext>(options =>
options.UseSqlServer(connectionString));
*/


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
}
else
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseBlazorFrameworkFiles();
app.UseStaticFiles(); 

app.UseRouting();


app.MapRazorPages();
app.MapControllers();
app.MapFallbackToFile("index.html");

app.Run();
