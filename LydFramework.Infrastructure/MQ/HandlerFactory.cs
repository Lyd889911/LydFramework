using LydFramework.Infrastructure.MQ.Handlers;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LydFramework.Infrastructure.MQ
{
    public class HandlerFactory
    {
        private readonly IHandler _defaultHandler;
        public HandlerFactory(IEnumerable<IHandler> handlers)
        {
            _defaultHandler = handlers.First(x => x.GetType().Name=="DefaultHandler");
        }
        public IHandler Create(string queue)
        {
            switch (queue)
            {
                case "Default":return _defaultHandler;
                default: return _defaultHandler;
            }
        }
    }
}
