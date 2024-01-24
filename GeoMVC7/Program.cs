using GeoMVC7.Domain;
using GeoMVC7.Domain.Entities;
using GeoMVC7.Domain.Repos;
using GeoMVC7.Domain.Repos.Base;
using GeoMVC7.Domain.Repos.Interfaces;
using GeoMVC7.Service;
using Microsoft.AspNetCore.Localization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System.Globalization;
using Microsoft.Extensions.Localization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Identity;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.


builder.Services.AddControllersWithViews();
builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddControllers().AddNewtonsoftJson();

builder.Services.AddScoped(typeof(IRepo<>), typeof(BaseRepo<>));
builder.Services.AddScoped<IMyGeometryRepo, MyGeometryRepo>();
builder.Services.AddScoped<IMyPageRepo, MyPageRepo>();
builder.Services.AddTransient<IEmailSender, EmailSender>();
//builder.Services.Configure<RequestLocalizationOptions>(options => {
//    var supportedCultures = new[]
//    {
//        new CultureInfo("en"),
//        new CultureInfo("de"),
//        new CultureInfo("fr"),
//        new CultureInfo("es"),
//        new CultureInfo("ru"),
//        new CultureInfo("ja"),
//        new CultureInfo("ar"),
//        new CultureInfo("zh"),
//        new CultureInfo("en-GB")
//    };
//    options.DefaultRequestCulture = new RequestCulture("en-GB");
//    options.SupportedCultures = supportedCultures;
//    options.SupportedUICultures = supportedCultures;
//});
builder.Services.AddTransient<FileManagerService>();
builder.Services.AddTransient<PointLayersService>();
builder.Services.AddScoped<GeoService>();

builder.Configuration.AddJsonFile("appsettings.json");
builder.Services.AddDbContext<ApplicationContext>(options =>
{
    //options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection"), x => x.UseNetTopologySuite());
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection"));
});
builder.Services.AddDefaultIdentity<ApplicationUser>(options =>
{
    options.SignIn.RequireConfirmedAccount = true;
    options.Password.RequiredLength = 4;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireDigit = false;
    options.Password.RequireLowercase = false;
    options.Password.RequireUppercase = false;

}).AddEntityFrameworkStores<ApplicationContext>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}

app.UseStaticFiles();


var supportedCultures = new[]
            {
                new CultureInfo("en-US"),
                new CultureInfo("en-GB"),
                new CultureInfo("en"),
                new CultureInfo("de-DE"),
                new CultureInfo("de")
            };

app.UseRequestLocalization(new RequestLocalizationOptions
{
    DefaultRequestCulture = new RequestCulture("en-US"),
    SupportedCultures = supportedCultures,
    SupportedUICultures = supportedCultures
});

app.UseRouting();

app.UseCors();

app.UseAuthentication();

app.UseAuthorization();

app.MapAreaControllerRoute(name: "Admin", areaName: "Admin", pattern: "admin/{controller=Page}/{action=Index}/{id?}");

app.MapControllerRoute(name: "default",pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();

app.Run();
