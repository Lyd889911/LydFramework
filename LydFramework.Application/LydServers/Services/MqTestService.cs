using LydFramework.Application.Contracts.LydServers.MqTest;
using LydFramework.Domain;
using LydFramework.Domain.Shared.Enums;
using LydFramework.Module.Dependencys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LydFramework.Application.LydServers.Services
{
    public class MqTestService : IMqTestService, IScopedDependency
    {
        private readonly IEventBus _eventBus;
        public MqTestService(IEventBus eventBus)
        {
            _eventBus = eventBus;
        }

        public string Publish(string eventName, string msg)
        {
            _eventBus.Publish(eventName, msg);
            return DateTime.Now.ToString();
        }
    }
}
