using LydFramework.Application.Contracts.Menus;
using LydFramework.Application.Contracts.Roles;
using LydFramework.Application.Contracts.Users;
using LydFramework.Application.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ApplicationExpansions
    {
        public static IServiceCollection AddApplication(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());

            #region 批量注册Service
            var sp = services.BuildServiceProvider();
            ILoggerFactory factory = sp.GetRequiredService<ILoggerFactory>();
            ILogger logger = factory.CreateLogger("RegisterApplication");
            StringBuilder sb = new StringBuilder();
            Type[] types = Assembly.GetExecutingAssembly().GetTypes();
            foreach (Type type in types)
            {
                string typeName = type.Name;
                if (typeName.EndsWith("Service"))
                {
                    Type? interf = type.GetInterface($"I{typeName}");
                    if (interf != null)
                    {
                        services.AddScoped(interf, type);
                        sb.AppendLine($"{interf.Name}:{type.Name}");
                    }
                }
            }
            logger.LogInformation(sb.ToString());
            #endregion

            return services;
        }
    }
}
