using LydFramework.Domain.Shared.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace LydFramework.RabbitMQ
{
    public class EventBusSubscriptionsManager
    {
        /// <summary>
        /// 事件名：rabbitmq里面就是路由键
        /// 事件处理器集合：rabbitmq里面就是队列名
        /// </summary>
        private Dictionary<string, List<IEventHandler>> _handlers;
        public EventBusSubscriptionsManager()
        {
            _handlers = new Dictionary<string, List<IEventHandler>>();
            //var type = typeof(EventName);
            //if (type.IsEnum)
            //{
            //    FieldInfo[] fieldInfos = type.GetFields();
            //    foreach (FieldInfo fieldInfo in fieldInfos)
            //    {
            //        if (fieldInfo.IsLiteral)
            //        {
            //            Console.WriteLine("获取到的值：" + fieldInfo.Name);
            //            _handlers.Add(fieldInfo.Name, new List<IEventHandler>());
            //        }
            //    }
            //}
        }
        /// <summary>
        /// 添加事件的订阅处理
        /// </summary>
        /// <param name="eventName"></param>
        /// <param name="handler"></param>
        /// <exception cref="ArgumentException"></exception>
        public void AddSubscription(string eventName, IEventHandler handler)
        {
            if (!HasSubscriptionsForEvent(eventName))
            {
                _handlers.Add(eventName,new List<IEventHandler>());
            }
            if (_handlers[eventName].Any(s => s.EventHandlerName == handler.EventHandlerName))
            {
                throw new ArgumentException($"事件:{eventName},已经存在处理器:{handler.GetType().Name}");
            }
            _handlers[eventName].Add(handler);
        }

        /// <summary>
        /// 该事件是否有订阅
        /// </summary>
        /// <param name="eventName"></param>
        /// <returns></returns>
        public bool HasSubscriptionsForEvent(string eventName) 
            => _handlers.ContainsKey(eventName);
    }
}
