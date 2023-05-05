using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LydFramework.Infrastructure.MQ.Handlers
{
    public interface IHandler
    {
        Task Handle(string message);
    }
}
