using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Diagnostics;
using FluentValidation;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using Microsoft.AspNetCore.Identity;
using MyBiking.Application.Mapper;
using MyBiking.Application.Validation;
using MyBiking.Application.Models;
using MyBiking.Entity.Models;
using MediatR;
using MyBiking.Application.Functions.Command.User;
using System.Reflection;


namespace MyBiking.Application.Extensions
{
    public static class ServiceCollectionExtension
    {
        public static void AddApplication(this IServiceCollection services)
        {
            services.AddMediatR(cfg=>cfg.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly()));
            services.AddAutoMapper(typeof(RideDtoProfile), typeof(AuthUserDtoProfile));
            services.AddValidatorsFromAssemblyContaining<RegisterUserDtoRule>()
                .AddFluentValidationAutoValidation()
                .AddFluentValidationClientsideAdapters();

            services.ConfigureApplicationCookie(options =>
            {
                //options.Cookie.Name = "YourAppNameCookie";
                //options.Cookie.HttpOnly = true;
                //options.ExpireTimeSpan = TimeSpan.FromMinutes(30); // Adjust expiration time as needed
                options.LoginPath = "/Auth/Login"; // Specify your login path
                //options.AccessDeniedPath = "/Account/AccessDenied"; // Specify your access denied path
                //options.SlidingExpiration = true;
            });

            services.AddScoped<IPasswordHasher<ApplicationUser>, PasswordHasher<ApplicationUser>>();
        }
    }
}
