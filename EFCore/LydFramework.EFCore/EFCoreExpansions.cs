using EntityFrameworkCore.Core;
using LydFramework.EFCore.DbContexts;
using LydFramework.EFCore.Middlewares;
using LydFramework.EFCore.Repositorys;
using Microsoft.AspNetCore.Builder;
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
        /// 添加Dbcontext和工作单元
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

        /// <summary>
        /// 使用自动工作单元中间件
        /// </summary>
        public static void UseUnitOfWorkMiddleware(this IApplicationBuilder app)
        {
            app.UseMiddleware<UnitOfWorkMiddleware>();
        }
    }
}
