using InforceTask.Data;
using InforceTask.Services;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

var configuration = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json")
    .Build();

builder.Services.AddDbContext<UrlsDbContext>(options =>
{
    string? connection = configuration.GetConnectionString("LocalConnection");
    options.UseSqlServer(connection);
});

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.Cookie.Name = "InforceCookie";
        options.Cookie.HttpOnly = false;
        
    });
builder.Services.AddAuthorization();

builder.Services.AddTransient<UrlService>();
builder.Services.AddTransient<AuthService>();
builder.Services.AddTransient<IndexService>();

builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseAuthentication();
app.UseRouting();
app.UseAuthorization();

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseCors(options =>
{
    string? origin = configuration.GetValue<string>("Origins:LocalOrigin");

    options.WithOrigins(origin)
        .AllowAnyHeader()
        .AllowAnyMethod()
        .AllowCredentials();
});

app.MapControllerRoute(
    name: "default",
    pattern: "{controller}/{action=Index}/{id?}");

app.MapFallbackToFile("index.html");

app.Run();
