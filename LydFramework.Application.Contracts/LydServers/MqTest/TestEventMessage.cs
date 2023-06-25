using LydFramework.Domain.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LydFramework.Application.Contracts.LydServers.MqTest
{
    public class TestEventMessage : EventBusMessage
    {
        public TestEventMessage(object msgData) : base(msgData)
        {
        }
    }
}
