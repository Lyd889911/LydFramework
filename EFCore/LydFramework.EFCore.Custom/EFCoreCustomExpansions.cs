using LydFramework.EFCore.DbContexts;
using LydFramework.EFCore.MySql;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class EFCoreCustomExpansions
    {
        public static IServiceCollection AddEFCoreCustom<TDbContext>
                    (this IServiceCollection services, IConfiguration configuration)
                    where TDbContext : DbContext
        {
            services.AddEFCoreMySql<AuthDbContext>(configuration["DbConnection"], configuration["DbVersion"]);
            services.AddRepository();
            return services;
        }
    }
}
