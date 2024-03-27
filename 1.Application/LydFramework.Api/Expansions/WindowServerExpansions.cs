using Serilog;
using System.Diagnostics;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.ServiceProcess;
using System.Text;

namespace LydFramework.Api.Expansions
{
    public static class WindowServerExpansions
    {
        public static async Task InstallWindowServer(this IHostBuilder host, IConfiguration configuration)
        {
            bool enableWindowsService = Convert.ToBoolean(configuration["Application:EnableWindowsService"]);
            if (!enableWindowsService || !RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                return;

            host.UseWindowsService();

            string serviceName = configuration["Application:Name"];
            string appDescription = configuration["Application:Description"];
            string appPath = AppContext.BaseDirectory + Assembly.GetEntryAssembly().GetName().Name + ".exe";
            StringBuilder sc = new StringBuilder();

            // 获取所有的win服务
            var service = ServiceController.GetServices();

            // 如果已经安装,就卸载重新安装
            if (service.Any(x => x.ServiceName == serviceName))
            {
                Log.Logger.Warning("已经存在该Windows服务，不再重新创建");
                return;
            }

            #region 启动cmd
            ProcessStartInfo startInfo = new ProcessStartInfo
            {
                FileName = "cmd.exe",
                RedirectStandardOutput = true,// 由调用程序获取输出信息
                UseShellExecute = false,// 是否使用操作系统shell启动
                CreateNoWindow = true,// 不显示程序窗口
                RedirectStandardInput = true,// 接受来自调用程序的输入信息
                RedirectStandardError = true// 重定向标准错误输出
            };
            using var process = new Process();
            process.StartInfo = startInfo;
            process.Start();
            #endregion

            sc.Append($"sc create {serviceName} binPath={appPath} start=auto & ");
            sc.Append($"sc description {serviceName} \"{appDescription}\" & ");
            sc.Append($"exit");
            Log.Logger.Information($"创建Windows服务：{sc.ToString()}");
            await process.StandardInput.WriteLineAsync(sc.ToString());

            await process.WaitForExitAsync();
        }
    }
}
