using Hangfire;
using Hangfire.Redis.StackExchange;
using LydFramework.Hangfire.Jobs;
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
            string storage = configuration["Hangfire:Storage"].Replace(" ", "");
            string connection = configuration["Hangfire:Connection"];
            if ("redis".Equals(storage, StringComparison.CurrentCultureIgnoreCase))
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
            else if("sqlserver".Equals(storage, StringComparison.CurrentCultureIgnoreCase))
            {
                services.AddHangfire(config =>
                {
                    config.UseSqlServerStorage(connection);
                });
            }

            services.AddHangfireServer();
            services.AddHostedService<Job1>();
        }
    }
}