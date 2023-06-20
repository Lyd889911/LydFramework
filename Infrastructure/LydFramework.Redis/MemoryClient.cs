using LydFramework.Domain;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LydFramework.Redis
{
    public class MemoryClient: ICacheClient
    {
        private readonly IMemoryCache _memory;
        public MemoryClient(IMemoryCache memory)
        {
            _memory = memory;
        }

        public Task<T> Get<T>(string key)
        {
            return Task.FromResult(_memory.Get<T>(key));
        }

        public async Task<T> GetOrCreate<T>(string key, Func<T> func)
        {
            return await _memory.GetOrCreateAsync(key, (cacheentity) =>
            {
                return Task.Run(() =>
                {
                    var t = func();
                    return t;
                });

            });
        }

        public Task Remove(string key)
        {
            _memory.Remove(key);
            return Task.CompletedTask;
        }

        public Task Set(string key, object value, TimeSpan expiry = default)
        {
            if(expiry==default)
                _memory.Set(key, value);
            else
                _memory.Set(key, value, expiry);
            return Task.CompletedTask;
        }

        public Task Set(string key, object value, TimeSpan expiry = default, Domain.Shared.Enums.CacheDataType dataType = Domain.Shared.Enums.CacheDataType.String)
        {
            throw new NotImplementedException();
        }
    }
}
