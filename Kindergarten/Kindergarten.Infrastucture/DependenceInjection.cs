using Kindergarten.Application.Abstractions;
using Kindergarten.Domain.Entities;
using Kindergarten.Infrastucture.Abstractions;
using Kindergarten.Infrastucture.Persistance;
using Kindergarten.Infrastucture.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Kindergarten.Infrastucture
{
    public static class DependenceInjection 
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseNpgsql(configuration.GetConnectionString("DefaultConnection"));
            });

            services.AddScoped<IApplicationDbContext, ApplicationDbContext>();
            services.AddScoped<ITokenService, JwtService>();
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IHashService, HashServise>();


            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
              .AddJwtBearer(options =>
              {
                  options.TokenValidationParameters = new TokenValidationParameters
                  {
                      ValidateIssuer = true,
                      ValidateIssuerSigningKey = true,
                      ValidateLifetime = true,
                      ValidateAudience = true,
                      ValidIssuer = configuration["JWT:ValidIssuer"],
                      ValidAudience = configuration["JWT:validAudiense"],
                      IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:Secret"]!))
                  };
              });

            services.AddAuthorization(options =>
            {
                options.AddPolicy("AdminActions", polisy =>
                {
                    polisy.RequireClaim("Roles", Domain.Enums.Roles.Admin.ToString());
                });
            });

            services.AddAuthorization(options =>
            {
                options.AddPolicy("TeacherActions", polisy =>
                {
                    polisy.RequireClaim("Roles", Domain.Enums.Roles.Teacher.ToString());
                });
            });
            
            services.AddAuthorization(options =>
            {
                options.AddPolicy("ChildernActions", polisy =>
                {
                    polisy.RequireClaim("Roles", Domain.Enums.Roles.Childern.ToString());
                });
            });

            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

            return services;
        }
    }
}
