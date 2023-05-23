using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LydFramework.Infrastructure.MQ.Consumers
{
    public class RabbitMqConsumerFactory
    {
        private IModel _channel;
        public RabbitMqConsumerFactory(IModel channel)
        {
            _channel = channel;
        }
        public EventingBasicConsumer Create(string queue)
        {
            var consumer = new EventingBasicConsumer(_channel);

            consumer.Received += (model, ea) => {
                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                Console.WriteLine("已接收到消息: {0}", message);

                //TODO：根据不同的消费队列，做出不同消费方法选择
                switch (queue)
                {
                    case "Default": break;
                }

                _channel.BasicAck(ea.DeliveryTag, false);
            };

            return consumer;
        }
    }
}
