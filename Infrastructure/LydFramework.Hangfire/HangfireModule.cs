using Hangfire;
using Hangfire.Redis.StackExchange;
using LydFramework.Module;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace LydFramework.Hangfire
{
    public class HangfireModule:LydModule
    {
        public override void ConfigureServices(IServiceCollection services)
        {
            var configuration = services.BuildServiceProvider().GetRequiredService<IConfiguration>();
            string storage = configuration["Hangfire:Storage"].ToLower().Replace(" ", "");
            string connection = configuration["Hangfire:Connection"];
            bool isEnabled = Convert.ToBoolean(configuration["Hangfire:IsEnabled"]);
            if (isEnabled)
            {
                if (storage=="redis")
                {
                    var options = new RedisStorageOptions
                    {
                        Prefix = "hangfire:",
                        Db = 0
                    };
                    services.AddHangfire(configuration =>
                    {
                        configuration.UseRedisStorage(connection, options);
                    });
                }
                else if (storage== "sqlserver")
                {
                    services.AddHangfire(config =>
                    {
                        config.UseSqlServerStorage(connection);
                    });
                }

                services.AddHangfireServer();
                services.AddHostedService<HostedService>();
            }

        }
    }
}