using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LydFramework.Infrastructure.MQ
{
    public class MQOptions
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string VirtualHost { get; set; }
        public string HostName { get; set; }
        public bool HasConsumer { get; set; }
        public List<string> Queues { get; set; }
    }
}
