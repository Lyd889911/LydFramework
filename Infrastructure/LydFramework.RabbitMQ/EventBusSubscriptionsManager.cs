using LydFramework.Domain.Shared.Enums;
using LydFramework.RabbitMQ.Handlers;
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
        /// <summary>
        /// key：处理器名字
        /// value：事件名字集合
        /// </summary>
        private Dictionary<string, List<string>> _events;
        public EventBusSubscriptionsManager()
        {
            _handlers = new Dictionary<string, List<IEventHandler>>();
            InitSubscriptions();
        }
        /// <summary>
        /// 初始化订阅事件列表
        /// </summary>
        private void InitSubscriptions()
        {
            var handlers = Assembly.GetAssembly(typeof(IEventHandler))
                .GetTypes().Where(t => t.IsAssignableTo(typeof(IEventHandler)) && !t.IsInterface);
            foreach (var handler in handlers)
            {
                var handlerobj = Activator.CreateInstance(handler) as IEventHandler;
                if (handlerobj != null)
                {
                    AddSubscription(handlerobj);
                }
            }
        }
        /// <summary>
        /// 添加事件的订阅处理
        /// </summary>
        /// <param name="eventName"></param>
        /// <param name="handler"></param>
        /// <exception cref="ArgumentException"></exception>
        public void AddSubscription(IEventHandler handler)
        {
            if (!HasSubscriptionsForEvent(handler.EventName))
            {
                _handlers.Add(handler.EventName, new List<IEventHandler>());
            }
            if (_handlers[handler.EventName].Any(s => s.EventHandlerName == handler.EventHandlerName))
            {
                Console.WriteLine($"事件:{handler.EventName},已经存在处理器:{handler.GetType().Name}");
                return;
            }
            _handlers[handler.EventName].Add(handler);
        }

        /// <summary>
        /// 该事件是否有订阅
        /// </summary>
        /// <param name="eventName"></param>
        /// <returns></returns>
        public bool HasSubscriptionsForEvent(string eventName) 
            => _handlers.ContainsKey(eventName);

        public Dictionary<string, List<IEventHandler>> GetHandlers() => _handlers;

    }
}
