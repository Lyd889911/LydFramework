using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LydFramework.Domain.InfrastructureContracts
{
    public interface ICacheFactoryProvider
    {
        ICacheProvider Provider(string cacheid);
    }
}
