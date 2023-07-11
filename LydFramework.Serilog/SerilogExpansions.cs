using Microsoft.Extensions.Hosting;
using Serilog.Events;
using Serilog;
using System.Runtime.CompilerServices;
using Microsoft.Extensions.Configuration;
using LydFramework.Serilog;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class SerilogExpansions
    {
        public static IHostBuilder UseLydSerilog(this IHostBuilder host,IConfiguration configuration)
        {
            var option = configuration.GetRequiredSection("Serilog").Get<SerilogOption>();
            if(option == null)
            {
                throw new Exception("Serilog的option为空");
            }


            LoggerConfiguration loggerConfiguration = new LoggerConfiguration()
                .MinimumLevel.Override("Microsoft.AspNetCore", LogEventLevel.Warning)
                .MinimumLevel.Debug()
                .Enrich.FromLogContext();

            if (option.WriteToFile)
            {
                loggerConfiguration = loggerConfiguration.WriteTo.Async(x => x.File(path:option.FilePath, 
                            rollingInterval: option.RollingInterval,
                            outputTemplate: option.SerilogOutputTemplate,
                            retainedFileCountLimit: option.FileCountLimit
                            ));
            }

            if (option.WriteToConsole)
            {
                loggerConfiguration = loggerConfiguration.WriteTo.Async(x => x.Console());
            }

            Log.Logger = loggerConfiguration.CreateLogger();

            host.UseSerilog();
            return host;
        }
    }
}