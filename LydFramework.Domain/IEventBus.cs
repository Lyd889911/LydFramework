using LydFramework.Domain.Shared;
using LydFramework.Domain.Shared.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LydFramework.Domain
{
    public interface IEventBus
    {
        public void Publish(string routingKey,object data);
    }
}
