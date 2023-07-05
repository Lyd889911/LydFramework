using LydFramework.Application.Contracts.LydServers.MqTest;
using LydFramework.Domain.InfrastructureContracts;
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
        private readonly IEventBusProvider _eventBus;
        public MqTestService(IEventBusProvider eventBus)
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
