
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using MyBiking.Application.Models;
using MyBiking.Entity.Models;
using MyBiking.Infrastructure.Repository;



//using MyBiking.Domain.Enitity;
//using MyBiking.Infrastructure.Repository;
//using MyBiking.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyBiking.Infrastructure.Extensions
{
    public static class ServiceCollectionExtension
    {
        public static void AddInfrastructure(this IServiceCollection services,IConfiguration configuration)
        {

            services.AddDbContext<MyBikingDbContext>(opt =>
                opt.UseSqlServer(configuration.GetConnectionString("MyBikingDbConnection")));

            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<MyBikingDbContext>()
                .AddDefaultTokenProviders();


            //var authenticationSettings = configuration.GetSection("Authentication").Get<AuthenticationSettings>();

            //services.AddAuthentication(options =>
            //{
            //    options.DefaultAuthenticateScheme = "Bearer";
            //    options.DefaultChallengeScheme = "Bearer";
            //    options.DefaultScheme = "Bearer";
            //});

            
            //.AddJwtBearer(options =>
            //{
            //    options.RequireHttpsMetadata = false;
            //    options.SaveToken = true;
            //    options.TokenValidationParameters = new TokenValidationParameters()
            //    {
            //        ValidIssuer = authenticationSettings.JwtIssuer,
            //        ValidAudience = authenticationSettings.JwtIssuer,
            //        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(authenticationSettings.JwtKey))
            //    };
            //});

            services.AddScoped<IMyBikingRepository, MyBikingRepository>();
            services.AddScoped<MyBikingDbSeeder>();
            //services.AddSingleton<AuthenticationSettings>(authenticationSettings);


        }
    }
}
