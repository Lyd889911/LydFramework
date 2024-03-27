using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LydFramework.Domain.InfrastructureContracts
{
    public interface ISqlSugarRepository<T>: ISugarRepository, ISimpleClient<T> where T : class, new()
    {
    }
}
