using LydFramework.Domain.Shared.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LydFramework.Domain
{
    public interface ICacheClient
    {
        public Task<T> Get<T>(string key);
        public Task Set(string key, object value, TimeSpan expiry = default, CacheDataType dataType = CacheDataType.String);
        public Task Remove(string key);
        public Task<T> GetOrCreate<T>(string key, Func<T> func);
    }
}
