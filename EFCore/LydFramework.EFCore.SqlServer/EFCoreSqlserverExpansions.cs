using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class EFCoreSqlserverExpansions
    {
        public static IServiceCollection AddEFCoreSqlServer<TDbContext>
            (this IServiceCollection services, string connection, string dbversion)
            where TDbContext:DbContext
        {
            services.AddEFCore<TDbContext>(opt =>
            {

                if (dbversion == "2008" || dbversion == "2005")
                {
                    opt.ReplaceService<IQueryTranslationPostprocessorFactory, SqlServer2008QueryTranslationPostprocessorFactory>();
                }
                opt.UseSqlServer(connection, x => x.MigrationsAssembly("LydFramework.EFCore.SqlServer"));
            });
            return services;
        }
    }
}