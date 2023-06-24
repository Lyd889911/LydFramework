using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LydFramework.RabbitMQ
{
    /// <summary>
    /// 事件的处理
    /// 一个事件处理器处理一个事件
    /// 换句话说，一个事件可以对应多个事件处理器
    /// </summary>
    public interface IEventHandler
    {
        /// <summary>
        /// 事件处理器的名字（MQ：队列名）
        /// </summary>
        string EventHandlerName
        {
            get
            {
                return this.GetType().Name.Replace("EventHandler", "");
            }
        }
        /// <summary>
        /// 事件名（MQ：路由键）
        /// </summary>
        string EventName { get; }
        /// <summary>
        /// 是否启动消费
        /// </summary>
        bool EnableConsume { get; }
        /// <summary>
        /// 该处理器的处理方法
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        Task Handle(string message);
    }
}
