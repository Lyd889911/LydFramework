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
        public Task Set(string key, object value, TimeSpan expiry = default);
        public Task Remove(string key);
    }
}
