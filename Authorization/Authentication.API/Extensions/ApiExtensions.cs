using System.Text;
using Authorization.API.Endpoints;
using Authorization.Application.Interfaces.Auth;
using Authorization.Application.Services;
using Authorization.Core.Enums;
using Authorization.Infrastructure;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;

namespace Authorization.API.Extensions
{
    public static class ApiExtensions
    {
        public static void AddMappedEndpoints(this IEndpointRouteBuilder app)
        {
            app.MapUsersEndpoints();
        }

        public static void AddApiAuthentication(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            services.Configure<JwtOptions>(configuration.GetSection(nameof(JwtOptions)));

            var jwtOptions = configuration.GetSection(nameof(JwtOptions)).Get<JwtOptions>();
            // схема как должна работать апи, когд к ней подключается какой то ползователь
            // конретно эта проверяет, что в заголовках передается вообще токен
            // Если будет схема куки, то будет через куки
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
                {
                    options.TokenValidationParameters = new()
                    {
                        ValidateIssuer = false,
                        ValidateAudience = false,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(
                            Encoding.UTF8.GetBytes(jwtOptions!.SecretKey))
                    };

                    options.Events = new JwtBearerEvents
                    {
                        OnMessageReceived = context =>
                        {
                            context.Token = context.Request.Cookies["tasty-cookies"];

                            return Task.CompletedTask;
                        }
                    };
                });
            services.AddScoped<IPermissionService, PermissionService>();
            services.AddSingleton<IAuthorizationHandler, PermissionAuthorizationHandler>();

            services.AddAuthorization();
        }


        public static IEndpointConventionBuilder RequirePermissions<TBuilder>(
            this TBuilder builder, params Permissions[] permissions) where TBuilder : IEndpointConventionBuilder
        {
            return builder.RequireAuthorization(policy =>
                policy.AddRequirements(new PermissionRequirement(permissions)));
        }
    }
}
