using Serilog.Events;
using Serilog;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;
using LydFramework.Common.Utils;

namespace LydFramework.Common.Expansions
{
    public static class SerilogExpansions
    {
        public static IHostBuilder UseLydSerilog(this IHostBuilder host)
        {
            var logconfig = AppUtils.AppSettings.Serilog;

            LoggerConfiguration loggerConfiguration = new LoggerConfiguration()
                .MinimumLevel.Override("Microsoft.AspNetCore", LogEventLevel.Warning)
                .Filter.ByExcluding(logEvent => logEvent.Properties.ContainsKey("SourceContext") && logEvent.Properties["SourceContext"].ToString().Contains("Microsoft.EntityFrameworkCore.Database.Command"))
                .MinimumLevel.Information()
                .Enrich.FromLogContext();

            if (logconfig.WriteToFile)
            {
                loggerConfiguration = loggerConfiguration.WriteTo.Async(x => x.File(path: logconfig.FilePath,
                            rollingInterval: (RollingInterval)logconfig.RollingInterval,
                            outputTemplate: logconfig.SerilogOutputTemplate,
                            retainedFileCountLimit: logconfig.FileCountLimit
                            ));
            }

            if (logconfig.WriteToConsole)
            {
                loggerConfiguration = loggerConfiguration.WriteTo.Async(x => x.Console(outputTemplate: "[{Timestamp:HH:mm:ss} {Level:u3} {SourceContext}] {Message:lj}{NewLine}{Exception}"));
            }

            Log.Logger = loggerConfiguration.CreateLogger();

            host.UseSerilog();
            return host;
        }
    }
    public class SerilogOption
    {
        public bool WriteToFile { get; set; }
        public bool WriteToConsole { get; set; }
        public string FilePath { get; set; }
        public RollingInterval RollingInterval { get; set; }
        public int FileCountLimit { get; set; }
        public string SerilogOutputTemplate { get; set; }
        public List<string> Excludes { get; set; }
    }
}