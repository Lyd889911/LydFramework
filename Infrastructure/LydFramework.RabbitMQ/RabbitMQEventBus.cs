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
        private static IModel channel;
        private static IConnection connection;
        private static bool _isConn = false;
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

            bool b = _rabbitmqConnection.TryConnect();
            channel = _rabbitmqConnection.CreateModel();
            channel.ExchangeDeclare("LydExchange", ExchangeType.Direct);
            channel.QueueDeclare(queue: "Q1");
            channel.QueueDeclare(queue: "Q2");
            channel.QueueBind(queue: "Q1", exchange: "LydExchange", routingKey: "T1");
            channel.QueueBind(queue: "Q2", exchange: "LydExchange", routingKey: "T1");
            InitQueue();
        }

        private void InitQueue()
        {
            channel.ExchangeDeclare("LydExchange", ExchangeType.Direct);
            var type = typeof(EventName);
            if (type.IsEnum)
            {
                FieldInfo[] fieldInfos = type.GetFields();
                foreach(FieldInfo fieldInfo in fieldInfos)
                {
                    if (fieldInfo.IsLiteral)
                    {
                        Console.WriteLine("获取到的值："+fieldInfo.Name);
                    }
                }
            }
        }

        public void Publish(EventName routingKey,object data)
        {
            string str = JsonConvert.SerializeObject(data);
            byte[] body = Encoding.UTF8.GetBytes(str);
            string key = routingKey.ToString();
            lock (_lock)
            {
                channel.BasicPublish(exchange: "LydExchange", routingKey: key, body: body);
            }
        }


        //private void Consumer(List<string> queues)
        //{
        //    foreach (var queue in queues)
        //    {
        //        //TODO 根据队列不同，创建不同的消费者
        //        var consumer = CreateConsumer(queue);
        //        channel.BasicConsume(queue: queue, autoAck: false, consumer: consumer);
        //    }
        //}


        //private EventingBasicConsumer CreateConsumer(string queue)
        //{
        //    var consumer = new EventingBasicConsumer(channel);

        //    consumer.Received += (model, ea) => {
        //        var body = ea.Body.ToArray();
        //        var message = Encoding.UTF8.GetString(body);

        //        //根据不同的消费队列，做出不同消费方法选择
        //        IHandler handler = _handlerFactory.Create(queue);
        //        handler.Handle(message);

        //        channel.BasicAck(ea.DeliveryTag, false);
        //    };

        //    return consumer;
        //}
    }
}