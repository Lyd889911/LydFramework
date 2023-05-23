using LydFramework.Domain;
using LydFramework.Infrastructure.MQ.Consumers;
using LydFramework.Infrastructure.MQ.Handlers;
using Microsoft.Extensions.Configuration;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using Newtonsoft.Json;

namespace LydFramework.Infrastructure.MQ
{
    public class RabbitMqClient : IMqClient
    {
        private readonly IConfiguration _configuration;
        private readonly HandlerFactory _handlerFactory;
        private static IModel channel;
        private static IConnection connection;
        private static bool _isConn = false;
        private MQOptions _option;


        public RabbitMqClient(IConfiguration configuration, HandlerFactory handlerFactory)
        {
            _configuration = configuration;
            _handlerFactory = handlerFactory;
            _option = _configuration.GetSection("MQ").Get<MQOptions>();


            if (!_isConn)
            {
                ConnectionFactory factory = new ConnectionFactory
                {
                    UserName = _option.UserName,
                    Password = _option.Password,
                    VirtualHost = _option.VirtualHost,
                    HostName = _option.HostName,
                };
                connection = factory.CreateConnection();//创建连接
                Console.WriteLine($"连接是否打开:{connection.IsOpen}");
                channel = connection.CreateModel();//创建通道
                Console.WriteLine($"通道是否打开:{channel.IsOpen}");

                if(connection.IsOpen&& channel.IsOpen)
                {
                    _isConn = true;
                    foreach (string queue in _option.Queues)
                    {
                        channel.QueueDeclare(queue: queue);
                    }

                    if (_option.HasConsumer)
                    {
                        Consumer(_option.Queues);
                    }
                }
            }
        }


        public void Publish(string key,object message)
        {
            string str = JsonConvert.SerializeObject(message);
            byte[] body = System.Text.Encoding.UTF8.GetBytes(str);
            channel.BasicPublish(exchange: "", routingKey: key, body: body);
        }


        private void Consumer(List<string> queues)
        {
            foreach(var queue in queues)
            {
                //TODO 根据队列不同，创建不同的消费者
                var consumer = CreateConsumer(queue);
                channel.BasicConsume(queue: queue, autoAck: false, consumer: consumer);
            }
        }


        private EventingBasicConsumer CreateConsumer(string queue)
        {
            var consumer = new EventingBasicConsumer(channel);

            consumer.Received += (model, ea) => {
                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);

                //根据不同的消费队列，做出不同消费方法选择
                IHandler handler = _handlerFactory.Create(queue);
                handler.Handle(message);

                channel.BasicAck(ea.DeliveryTag, false);
            };

            return consumer;
        }
    }
}