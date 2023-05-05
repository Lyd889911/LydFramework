using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LydFramework.Infrastructure.MQ.Handlers
{
    public class DefaultHandler : IHandler
    {
        public Task Handle(string message)
        {
            Console.WriteLine($"默认处理消息：{message}");
            return Task.CompletedTask;
        }
    }
}
