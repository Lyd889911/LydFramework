using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LydFramework.Domain.InfrastructureContracts
{
    public interface IMemoryCacheProvider
    {
        T Get<T>(string key);
        void Set(string key,object value, TimeSpan? expiry = default);
        void Remove(string key);
        public Task<T> GetORrCreate<T>(string key, Func<Task<T>> func, TimeSpan? expiry = default);
    }
}
