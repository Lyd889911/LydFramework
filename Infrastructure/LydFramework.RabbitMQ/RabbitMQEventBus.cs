using LydFramework.Domain;
using LydFramework.Domain.Shared;
using LydFramework.Domain.Shared.Enums;
using Microsoft.AspNetCore.Connections;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Reflection;
using System.Text;

namespace LydFramework.RabbitMQ
{
    public class RabbitMQEventBus: IEventBus
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger _logger;
        private readonly RabbitMQConnection _rabbitmqConnection;
        private object _lock = new object();
        public EventBusSubscriptionsManager SubscriptionsManager;

        public RabbitMQEventBus(RabbitMQConnection rabbitmqConnection,
            IConfiguration configuration,
            ILogger<RabbitMQEventBus> _logger,
            EventBusSubscriptionsManager subscriptionsManager)
        {
            _configuration = configuration;
            _rabbitmqConnection = rabbitmqConnection;
            SubscriptionsManager = subscriptionsManager;
            _rabbitmqConnection.GetChannel().ExchangeDeclare("LydExchange", ExchangeType.Direct);

            InitQueueAndConsumer();
        }

        /// <summary>
        /// 初始化队列
        /// </summary>
        private void InitQueueAndConsumer()
        {
            var handlers = SubscriptionsManager.GetHandlers();
            foreach (var handler in handlers)
            {
                foreach (var handlerItem in handler.Value)
                {
                    Console.WriteLine($"现在声明队列：{handlerItem.EventHandlerName}，路由键：{handler.Key}");
                    _rabbitmqConnection.GetChannel()
                        .QueueDeclare(queue: handlerItem.EventHandlerName,exclusive:false);

                    _rabbitmqConnection.GetChannel()
                        .QueueBind(queue: handlerItem.EventHandlerName, exchange: "LydExchange", routingKey: handlerItem.EventName);
                    //是否需要在本项目消费该消息
                    if (handlerItem.EnableConsume)
                    {
                        Console.WriteLine($"创建消费者：{handlerItem.EventHandlerName}");
                        //TODO 根据队列不同，创建不同的消费者
                        var consumer = CreateConsumer(handlerItem);
                        _rabbitmqConnection.GetChannel()
                            .BasicConsume(queue: handlerItem.EventHandlerName, autoAck: false, consumer: consumer);
                    }
                }
            }
        }

        public void Publish(string eventName,object data)
        {
            string str = JsonConvert.SerializeObject(data);
            byte[] body = Encoding.UTF8.GetBytes(str);
            lock (_lock)
            {
                _rabbitmqConnection.GetChannel()
                    .BasicPublish(exchange: "LydExchange", routingKey: eventName, body: body);
            }

        }



        private EventingBasicConsumer CreateConsumer(IEventHandler handler)
        {
            var consumer = new EventingBasicConsumer(_rabbitmqConnection.GetChannel());

            consumer.Received += async (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);

                //根据不同的消费队列，做出不同消费方法选择
                await handler.Handle(message);

                _rabbitmqConnection.GetChannel().BasicAck(ea.DeliveryTag, false);
            };

            return consumer;
        }
    }
}