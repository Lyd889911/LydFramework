using LydFramework.Domain;
using LydFramework.Module;
using LydFramework.RabbitMQ.Handlers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LydFramework.RabbitMQ
{
    public class RabbitMQModule:LydModule
    {
        public override void ConfigureServices(IServiceCollection services)
        {
            var sp = services.BuildServiceProvider();
            var configuration = sp.GetRequiredService<IConfiguration>();
            ILogger<RabbitMQConnection> logger = sp.GetRequiredService<ILogger<RabbitMQConnection>>();
            ConnectionFactory factory = new ConnectionFactory
            {
                UserName = configuration["RabbitMQ:UserName"],
                Password = configuration["RabbitMQ:Password"],
                VirtualHost = configuration["RabbitMQ:VirtualHost"],
                HostName = configuration["RabbitMQ:HostName"],
            };
            var rabbitmqConnection = new RabbitMQConnection(factory,logger);
            services.AddSingleton(rabbitmqConnection);
            var subscriptionManager = new EventBusSubscriptionsManager();
            services.AddSingleton(subscriptionManager);
            services.AddSingleton<IEventBus, RabbitMQEventBus>();
        }
        public void InitEventHandler(EventBusSubscriptionsManager subscriptionManager)
        {
            subscriptionManager.AddSubscription("T1", new Print1EventHandler("print1"));
            subscriptionManager.AddSubscription("T2", new Print2EventHandler("print1"));
            subscriptionManager.AddSubscription("T3", new Print3EventHandler("print1"));
        }
    }
}
