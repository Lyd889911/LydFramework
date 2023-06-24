using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LydFramework.RabbitMQ.Handlers
{
    public class Print1EventHandler : IEventHandler
    {
        public string EventName => "Print";

        public bool EnableConsume => true;

        public Task Handle(string message)
        {
            Console.WriteLine("==============================================");
            Console.WriteLine("这是1号处理器");
            Console.WriteLine("==============================================");
            return Task.CompletedTask;
        }
    }
}
