using MyBiking.Infrastructure.Extensions;
using MyBiking.Application.Extensions;
using MyBiking.Infrastructure.Repository;
using Microsoft.AspNetCore.Identity;
using static System.Formats.Asn1.AsnWriter;
using MyBiking.Entity.Models;
using Microsoft.AspNetCore.Localization;
using System.Globalization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.


builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddApplication();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();
var supportedCultures = new[]
{
        new CultureInfo("en-US"),
        //new CultureInfo("pl-PL"),
    };

app.UseRequestLocalization(new RequestLocalizationOptions
{
    DefaultRequestCulture = new RequestCulture("en-US"),
    SupportedCultures = supportedCultures,
    SupportedUICultures = supportedCultures
});

app.UseSwagger();
app.UseSwaggerUI();
// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

var scope = app.Services.CreateScope();
var seeder = scope.ServiceProvider.GetRequiredService<MyBikingDbSeeder>();
seeder.Seed();

//using(var scopeForNationality = app.Services.CreateScope())
//{
//    var nationalityManago = scopeForNationality.ServiceProvider.GetRequiredService<MyBikingDbSeeder>();
//    nationalityManago.Seed();

//}

using (var scopeForRole = app.Services.CreateScope())
{
    var roleManager = scopeForRole.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
    var roles = new[] { "Admin", "Member" };

    foreach (var role in roles)
    {
        if (!await roleManager.RoleExistsAsync(role))
        {
            await roleManager.CreateAsync(new IdentityRole(role));
        }
    }
}



app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();
//app.MapRazorPages();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
public partial class Program { }