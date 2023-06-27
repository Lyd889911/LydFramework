using LydFramework.Application;
using LydFramework.Application.Filters;
using LydFramework.EFCore;
using LydFramework.Hangfire;
using LydFramework.Module;
using LydFramework.Module.Attributes;
using LydFramework.RabbitMQ;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace LydFramework.WebApi
{
    [DependOn(typeof(ApplicationModule),
        typeof(EFCoreModule),
        typeof(HangfireModule),
        typeof(RabbitMQModule))]
    public class ApiModule:LydModule
    {
        public override void ConfigureServices(IServiceCollection services)
        {
            services.AddMemoryCache();

            services.Configure<MvcOptions>(x =>
            {
                x.Filters.Add<ResponseFilter>();
            });

            services.AddHttpContextAccessor();

            var sp = services.BuildServiceProvider();
            var configuration = sp.GetRequiredService<IConfiguration>();

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
        }
    }
}
