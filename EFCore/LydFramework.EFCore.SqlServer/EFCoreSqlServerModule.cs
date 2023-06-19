using LydFramework.Module;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using LydFramework.Module.Attributes;
using LydFramework.EFCore.DbContexts;
using Microsoft.Extensions.Configuration;

namespace LydFramework.EFCore.SqlServer
{
    [DependOn(typeof(EFCoreModule))]
    public class EFCoreSqlServerModule:LydModule
    {
        public override void ConfigureServices(IServiceCollection services)
        {
            var sp = services.BuildServiceProvider();
            var config = sp.GetRequiredService<IConfiguration>();
            services.AddEFCoreSqlServer<AuthDbContext>(config["DbConnection"], config["DbVersion"]);
            //这里可以注册多个DbContext

        }
    }
}
