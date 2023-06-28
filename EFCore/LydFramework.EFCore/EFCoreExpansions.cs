using EntityFrameworkCore.Core;
using LydFramework.EFCore.UnitOfWorks;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.Extensions.Configuration;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class EFCoreExpansions
    {

        /// <summary>
        /// 添加Dbcontext和工作单元
        /// </summary>
        private static IServiceCollection AddEFCore<TDbContext>(this IServiceCollection services,
             Action<DbContextOptionsBuilder>? optionsAction = null,
             ServiceLifetime lifetime = ServiceLifetime.Scoped)
            where TDbContext : DbContext
        {
            services.AddDbContextFactory<TDbContext>(optionsAction, lifetime);
            services.AddTransient<IUnitOfWork,UnitOfWork<TDbContext>>();
            return services;
        }


        public static IServiceCollection AddEFCore<TDbContext>(this IServiceCollection services, IConfiguration configuration)
    where TDbContext : DbContext
        {
            var dbType = configuration["EFCore:DbType"].ToLower().Replace(" ", "");
            var dbConnection = configuration["EFCore:DbConnection"];
            var dbVersion = configuration["EFCore:DbVersion"];

            if (dbType == "sqlserver")
            {
                services.AddEFCore<TDbContext>(builder =>
                {
                    if (dbVersion == "2008" || dbVersion == "2005")
                    {
                        builder.ReplaceService<IQueryTranslationPostprocessorFactory, SqlServer2008QueryTranslationPostprocessorFactory>();
                    }
                    builder.UseSqlServer(dbConnection, x => x.MigrationsAssembly("CatalogServer.EFCore"));
                });

            }
            else if (dbType == "mysql")
            {
                services.AddEFCore<TDbContext>(builder =>
                {
                    builder.UseMySql(dbConnection, new MySqlServerVersion(dbVersion), x => x.MigrationsAssembly("CatalogServer.EFCore"));
                });
            }

            return services;
        }

        /// <summary>
        /// 使用自动工作单元中间件
        /// </summary>
        public static void UseUnitOfWorkMiddleware(this IApplicationBuilder app,IConfiguration configuration)
        {
            bool isenabled = Convert.ToBoolean(configuration["EFCore:IsEnabledUnitOfWork"]);
            if (isenabled)
            {
                app.UseMiddleware<UnitOfWorkMiddleware>();
            }
        }
    }
}
