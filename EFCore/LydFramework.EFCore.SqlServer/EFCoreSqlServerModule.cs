using LydFramework.Module;
using Microsoft.Extensions.DependencyInjection;
using LydFramework.Module.Attributes;
using Microsoft.Extensions.Configuration;
using LydFramework.EFCore.LydServers.DbContexts;

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
