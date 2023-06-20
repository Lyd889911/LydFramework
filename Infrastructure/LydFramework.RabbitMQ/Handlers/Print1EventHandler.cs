using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LydFramework.RabbitMQ.Handlers
{
    public class Print1EventHandler : IEventHandler
    {
        public string EventHandlerName { get; set; }
        public Print1EventHandler(string eventHandlerName)
        {
            EventHandlerName = eventHandlerName;
        }

        public Task Handle(string message)
        {
            Console.WriteLine("==============================================");
            Console.WriteLine("这是1号处理器");
            Console.WriteLine("==============================================");
            return Task.CompletedTask;
        }
    }
}
