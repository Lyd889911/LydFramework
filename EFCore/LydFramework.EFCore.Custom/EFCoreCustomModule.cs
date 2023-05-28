using LydFramework.EFCore.DbContexts;
using LydFramework.EFCore.MySql;
using LydFramework.Module;
using LydFramework.Module.Attributes;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace LydFramework.EFCore.Custom
{
    [DependOn(typeof(EFCoreMySqlModule))]
    public class EFCoreCustomModule:LydModule
    {
        public override void ConfigureServices(IServiceCollection services)
        {
            var sp = services.BuildServiceProvider();
            var config = sp.GetRequiredService<IConfiguration>();
            services.AddEFCoreMySql<AuthDbContext>(config["DbConnection"], config["DbVersion"]);
        }
    }
}