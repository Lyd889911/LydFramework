using Hangfire;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LydFramework.Hangfire.Jobs
{
    /// <summary>
    /// 引用了LydFramework.Application.Contracts
    /// 这里注入Service，执行定时任务，在Application层好执行各种操作
    /// </summary>
    public class Job1 : BackgroundService
    {
        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            RecurringJob.AddOrUpdate("",() => Console.WriteLine("每隔两分钟执行一次"), "0 0/2 * * * ?");
            return Task.CompletedTask;
        }
    }
}
