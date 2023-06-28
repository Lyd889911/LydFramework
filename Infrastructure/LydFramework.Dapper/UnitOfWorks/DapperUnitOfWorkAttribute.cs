using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LydFramework.Dapper.UnitOfWorks
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class DapperUnitOfWorkAttribute: Attribute
    {
    }
}
