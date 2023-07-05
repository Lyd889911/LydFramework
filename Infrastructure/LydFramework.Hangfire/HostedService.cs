using Hangfire;
using Microsoft.Extensions.DependencyInjection;
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
        private IEnumerable<IWorker> _workers;
        private readonly IServiceScope _serviceScope;
        public HostedService(IServiceScopeFactory serviceScopeFactory)
        {
            _serviceScope = serviceScopeFactory.CreateScope();
        }
        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _workers = _serviceScope.ServiceProvider.GetServices<IWorker>();
            foreach (var worker in _workers)
            {
                var option = new RecurringJobOptions();
                option.TimeZone = TimeZoneInfo.FindSystemTimeZoneById("China Standard Time");
                RecurringJob.AddOrUpdate(worker.WorkerName, () => worker.Work(), worker.Cron, option);
            }
            return Task.CompletedTask;
        }
    }
}
