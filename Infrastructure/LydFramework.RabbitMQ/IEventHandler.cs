using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LydFramework.RabbitMQ
{
    /// <summary>
    /// 事件的处理
    /// </summary>
    public interface IEventHandler
    {
        /// <summary>
        /// 事件处理器的名字，对应rabbitmq的队列名
        /// </summary>
        string EventHandlerName { get; set; }

        Task Handle(string message);
    }
}
