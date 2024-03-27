using Serilog;
using Serilog.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LydFramework.Common.Utils
{
    public static class LogUtils
    {
        private static Dictionary<string, ILogger> loggerPools = new();
        public static ILogger TenantLogger(string tenantid)
        {
            bool b = loggerPools.TryGetValue(tenantid, out var logger);
            if (!b)
            {
                var logconfig = AppUtils.AppSettings.Serilog;
                string path = $"Logs/{tenantid}/log.log";

                LoggerConfiguration loggerConfiguration = new LoggerConfiguration()
                    .MinimumLevel.Override("Microsoft.AspNetCore", LogEventLevel.Warning)
                    .MinimumLevel.Information()
                    .Enrich.FromLogContext();

                loggerConfiguration = loggerConfiguration.WriteTo.Async(x => x.File(path: path,
                                        rollingInterval: RollingInterval.Hour,
                                        outputTemplate: logconfig.SerilogOutputTemplate,
                                        retainedFileCountLimit: logconfig.FileCountLimit
                                        ));

                var l = loggerConfiguration.CreateLogger();
                loggerPools.Add(tenantid, l);
                return l;
            }
            else
                return logger;


        }
    }
}
