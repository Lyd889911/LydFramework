using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace LydFramework.Common.Utils
{
    /// <summary>
    /// 当前程序的工具类
    /// </summary>
    public static class AppUtils
    {
        public static HttpContext HttpContext => GetService<IHttpContextAccessor>()?.HttpContext!;
        public static IServiceCollection Services { get;private set; }
        public static IServiceProvider ServiceProvider => Services.BuildServiceProvider();
        public static AppSettings AppSettings => GetService<IConfiguration>().Get<AppSettings>();

        public static void Init(IServiceCollection services)
        {
            Services = services;
            //ServiceProvider = services.BuildServiceProvider();
        }

        private static T GetService<T>() where T : class
        {
            return ServiceProvider.GetRequiredService<T>();
        }


        /// <summary>
        /// 用户id
        /// </summary>
        public static long? UserId
        {
            get => HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier)==null?null:Convert.ToInt64(HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier));
        }
        /// <summary>
        /// 角色id
        /// </summary>
        public static string? RoleId
        {
            get => HttpContext.User.FindFirstValue(ClaimTypes.Role);
        }
        /// <summary>
        /// 租户id
        /// </summary>
        public static long? TenantId
        {
            get => HttpContext.User.FindFirstValue("TenantId")==null?null:Convert.ToInt64(HttpContext.User.FindFirstValue("TenantId"));
        }
    }
}
