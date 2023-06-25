using Hangfire;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace LydFramework.Hangfire
{
    /// <summary>
    /// 引用了LydFramework.Application.Contracts
    /// 这里注入Service，执行定时任务，在Application层好执行各种操作
    /// </summary>
    public class HostedService : BackgroundService
    {
        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var workers = Assembly.GetAssembly(typeof(IWorker))
                .GetTypes().Where(t => t.IsAssignableTo(typeof(IWorker)) && !t.IsInterface).ToList();
            foreach(var worker in workers)
            {
                var workerobj = Activator.CreateInstance(worker) as IWorker;
                if(workerobj==null)
                    continue;
                RecurringJob.AddOrUpdate(workerobj.WorkerName,
                    ()=>workerobj.Work(), workerobj.Cron,new RecurringJobOptions() { TimeZone= TimeZoneInfo.Local });
            }
            return Task.CompletedTask;
        }
    }
}
