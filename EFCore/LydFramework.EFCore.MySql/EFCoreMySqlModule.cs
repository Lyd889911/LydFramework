
using LydFramework.EFCore.LydServers.DbContexts;
using LydFramework.Module;
using LydFramework.Module.Attributes;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LydFramework.EFCore.MySql
{
    [DependOn(typeof(EFCoreModule))]
    public class EFCoreMySqlModule:LydModule
    {
        public override void ConfigureServices(IServiceCollection services)
        {
            var sp = services.BuildServiceProvider();
            var config = sp.GetRequiredService<IConfiguration>();
            services.AddEFCoreMySql<AuthDbContext>(config["DbConnection"], config["DbVersion"]);
            //这里可以注册多个DbContext

        }
    }
}
