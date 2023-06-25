using EntityFrameworkCore.Core;
using LydFramework.EFCore.UnitOfWorks;
using Microsoft.AspNetCore.Builder;


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
