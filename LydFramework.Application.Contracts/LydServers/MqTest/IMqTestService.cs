using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LydFramework.Application.Contracts.LydServers.MqTest
{
    public interface IMqTestService
    {
        public string Publish(string eventName,string msg);
    }
}
