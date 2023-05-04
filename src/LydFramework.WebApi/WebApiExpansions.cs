using LydFramework.Application;
using LydFramework.Application.Filters;
using LydFramework.Domain.Shared;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class WebApiExpansions
    {
        public static IServiceCollection AddWebApi(this IServiceCollection services,IConfiguration configuration)
        {
            services.Configure<MvcOptions>(x =>
            {
                x.Filters.Add<IdentityUserFilter>();
                x.Filters.Add<ResponseFilter>();
                x.Filters.Add<UnitOfWorkFilter>();
            });

            //services.AddAutoMapper(Assembly.GetExecutingAssembly());

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                    .AddJwtBearer(jwtoption =>
                    {
                        string SigningKey = configuration["JwtSecurityKey"];
                        byte[] keyBytes = Encoding.UTF8.GetBytes(SigningKey);
                        var secKey = new SymmetricSecurityKey(keyBytes);
                        jwtoption.TokenValidationParameters = new()
                        {
                            ValidateIssuer = false,
                            ValidateAudience = false,
                            ValidateLifetime = true,
                            ValidateIssuerSigningKey = true,
                            IssuerSigningKey = secKey
                        };
                    });
            return services;
        }
    }
}
