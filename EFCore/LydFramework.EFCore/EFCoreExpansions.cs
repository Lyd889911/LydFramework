using EntityFrameworkCore.Core;
using LydFramework.EFCore.DbContexts;
using LydFramework.EFCore.Repositorys;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Reflection;
using System.Text;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class EFCoreExpansions
    {
        /// <summary>
        /// 批量添加仓储
        /// </summary>
        public static IServiceCollection AddRepository(this IServiceCollection services)
        {
            var sp = services.BuildServiceProvider();
            ILoggerFactory factory = sp.GetRequiredService<ILoggerFactory>();

            ILogger logger = factory.CreateLogger("RegisterRepository");
            StringBuilder sb = new StringBuilder();
            Type[] types = Assembly.GetExecutingAssembly().GetTypes();
            foreach (Type type in types)
            {
                string typeName = type.Name;
                if (typeName.EndsWith("Repository"))
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

            return services;
        }

        /// <summary>
        /// 添加工作单元
        /// </summary>
        public static IServiceCollection AddEFCore<TDbContext>(this IServiceCollection services,
             Action<DbContextOptionsBuilder>? optionsAction = null,
             ServiceLifetime lifetime = ServiceLifetime.Scoped)
            where TDbContext : DbContext
        {
            services.AddDbContextFactory<TDbContext>(optionsAction, lifetime);
            services.AddTransient<IUnitOfWork,UnitOfWork<TDbContext>>();
            return services;
        }
    }
}
