using LydFramework.Application.Contracts.MqTest;
using LydFramework.Domain;
using LydFramework.Domain.Shared.Enums;
using LydFramework.Module.Dependencys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LydFramework.Application.Services
{
    public class MqTestService:IMqTestService, IScopedDependency
    {
        private readonly IEventBus _eventBus;
        public MqTestService(IEventBus eventBus)
        {
            _eventBus = eventBus;
        }

        public string Publish(string msg)
        {
            _eventBus.Publish(EventName.T1,msg);
            return DateTime.Now.ToString();
        }
    }
}
