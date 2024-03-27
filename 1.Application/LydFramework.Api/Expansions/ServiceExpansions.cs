
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using LydFramework.Common.Utils;
using System.Reflection;
using Hangfire;
using Lydong.DynamicApi;

namespace LydFramework.Api.Expansions
{
    public static class ServiceExpansions
    {
        public static void AddLydFrameworkService(this IServiceCollection services)
        {
            //添加控制器
            services.AddControllers();
            //动态api
            services.AddDynamicApi();
            //添加http客户端和内容
            services.AddHttpClient();
            services.AddHttpContextAccessor();
            //跨域
            services.AddCors(options => {
                options.AddDefaultPolicy(
                    builder2 =>
                    {
                        builder2.WithOrigins(AppUtils.AppSettings.Application.CorsWithOrigins.ToArray())
                            .SetIsOriginAllowed(_ => true)
                            .AllowAnyMethod()
                            .AllowAnyHeader()
                            .AllowCredentials();
                    });
            });
            //swagger接口文档
            services.AddLydSwagger();

            //jwt认证
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                    .AddJwtBearer(jwtoption =>
                    {
                        byte[] keyBytes = Encoding.UTF8.GetBytes(AppUtils.AppSettings.Application.JwtSecurityKey);
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
            //jwt鉴权策略
            //services.AddAuthorization(options =>
            //{
            //    options.AddPolicy("Apis", policy => policy.Requirements.Add(new ApisRequirement()));
            //});

            //sqlsugar
            services.AddLydSqlSugar();
            //应用层
            services.AddLydApplicationService();

            services.AddLydongHangfire();
        }
    }
}
