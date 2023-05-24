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
    public static class EFCoreMySqlExpansions
    {
        public static IServiceCollection AddEFCoreMySql<TDbContext>
                    (this IServiceCollection services, string connection,string dbversion)
                    where TDbContext : DbContext
        {
            services.AddEFCore<TDbContext>(opt =>
            {
                opt.UseMySql(connection, new MySqlServerVersion(dbversion),x=>x.MigrationsAssembly("LydFramework.EFCore.MySql"));
            });
            return services;
        }
    }
}
