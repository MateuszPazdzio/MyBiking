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


namespace MyBiking.Application.Extensions
{
    public static class ServiceCollectionExtension
    {
        public static void AddApplication(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(RideDtoProfile), typeof(AuthUserDtoProfile));
            services.AddValidatorsFromAssemblyContaining<RegisterUserDtoRule>()
                .AddFluentValidationAutoValidation()
                .AddFluentValidationClientsideAdapters();

            services.AddScoped<IPasswordHasher<User>, PasswordHasher<User>>();
        }
    }
}
