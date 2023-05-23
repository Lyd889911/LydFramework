using LydFramework.Domain;
using LydFramework.Infrastructure.MQ.Consumers;
using Microsoft.Extensions.Configuration;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

namespace LydFramework.Infrastructure.MQ
{
    public class RabbitMqClient : IMqClient
    {
        private readonly IConfiguration _configuration;
        private IModel channel;
        private IConnection connection;
        public RabbitMqClient(IConfiguration configuration)
        {
            _configuration = configuration;
            ConnectionFactory factory = new ConnectionFactory
            {
                UserName = _configuration["MQ:UserName"],
                Password = _configuration["MQ:Password"],
                VirtualHost = _configuration["MQ:VirtualHost"],
                HostName = _configuration["MQ:HostName"],
            };
            connection = factory.CreateConnection();//创建连接
            Console.WriteLine($"连接是否打开:{connection.IsOpen}");
            channel = connection.CreateModel();//创建通道
            Console.WriteLine($"通道是否打开:{channel.IsOpen}");

            List<string> queues = _configuration.GetValue<List<string>>("MQ:Queues");
            foreach (string queue in queues)
            {
                channel.QueueDeclare(queue: "Default");
            }
            
            bool hasConsumer = _configuration.GetValue<bool>("MQ:HasConsumer");
            if(hasConsumer)
            {
                Consumer(queues);
            }

        }
        public void Publish(object message)
        {
            throw new NotImplementedException();
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
                Console.WriteLine("已接收到消息: {0}", message);

                //TODO：根据不同的消费队列，做出不同消费方法选择
                switch (queue)
                {
                    case "Default": break;
                }

                channel.BasicAck(ea.DeliveryTag, false);
            };

            return consumer;
        }
    }
}