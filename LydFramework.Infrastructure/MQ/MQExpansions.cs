using LydFramework.Domain;
using LydFramework.Infrastructure.MQ;
using LydFramework.Infrastructure.MQ.Handlers;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class MQExpansions
    {
        public static IServiceCollection AddMQ(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddTransient<IHandler,DefaultHandler>();
            services.AddScoped<HandlerFactory>();
            services.AddScoped<IMqClient, RabbitMqClient>();
            return services;
        }
    }
}
