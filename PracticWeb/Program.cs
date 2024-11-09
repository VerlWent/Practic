using System.Net.Http.Headers;
using Refit;
using PracticeWeb.Classes.LocalClasses;
using PracticeWeb.Interfaces;
using Microsoft.AspNetCore.Authentication.Cookies;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddTransient<AuthenticatedHttpClientHandler>();

builder.Services.AddHttpContextAccessor();

builder
    .Services.AddRefitClient<IUserClient>()
    .ConfigureHttpClient(c =>
    {
        c.BaseAddress = new Uri("https://localhost:7157");
    })
    .AddHttpMessageHandler<AuthenticatedHttpClientHandler>();

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(option =>
    {
        option.LoginPath = "/Authorization";
        option.ExpireTimeSpan = TimeSpan.FromMinutes(15);
        option.SlidingExpiration = true;
        option.Cookie.HttpOnly = true;
        option.Cookie.SecurePolicy = CookieSecurePolicy.Always;
        option.Cookie.SameSite = SameSiteMode.Lax;
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

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
