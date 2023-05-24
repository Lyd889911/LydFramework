using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace LydFramework.EFCore.SqlServer
{
    public static class EFCoreSqlserverExpansions
    {
        public static IServiceCollection AddEFCoreSqlServer<TDbContext>
            (this IServiceCollection services, IConfiguration configuration)
            where TDbContext:DbContext
        {
            services.AddEFCore<TDbContext>(opt =>
            {

                if (configuration["DbVersion"] == "2008" || configuration["DbVersion"] == "2005")
                {
                    opt.ReplaceService<IQueryTranslationPostprocessorFactory, SqlServer2008QueryTranslationPostprocessorFactory>();
                }
                opt.UseSqlServer(configuration["DbConnection"]);
            });
            services.AddRepository();
            return services;
        }
    }
}