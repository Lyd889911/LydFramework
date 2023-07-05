using LydFramework.Domain.InfrastructureContracts;
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
            services.AddSingleton<EventBusSubscriptionsManager>();
            services.AddSingleton<IEventBusProvider, RabbitMQEventBus>();

            //懒加载
            bool lazy = Convert.ToBoolean(configuration["RabbitMQ:LazyInitialize"]);

        }
    }
}
