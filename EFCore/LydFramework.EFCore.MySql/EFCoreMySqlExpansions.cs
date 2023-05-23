using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LydFramework.EFCore.MySql
{
    public static class EFCoreMySqlExpansions
    {
        public static IServiceCollection AddEFCoreMySqlr<TDbContext>
                    (this IServiceCollection services, IConfiguration configuration)
                    where TDbContext : DbContext
        {
            services.AddDbContext<TDbContext>(opt =>
            {
                opt.UseMySql(configuration["DbConnection"], new MySqlServerVersion(configuration["DbVersion"]));
            });
            services.AddEFCore(configuration);
            return services;
        }
    }
}
