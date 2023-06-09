﻿using LydFramework.Domain.InfrastructureContracts;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LydFramework.MemoryCache
{
    public class MemoryCacheProvider : IMemoryCacheProvider
    {
        private readonly IMemoryCache _memory;
        public MemoryCacheProvider(IMemoryCache memory)
        {
            _memory = memory;
        }

        public void Remove(string key)
        {
            _memory.Remove(key);
        }

        public T Get<T>(string key)
        {
            return _memory.Get<T>(key);
        }

        public void Set(string key, object value, TimeSpan? expiry = default)
        {
            if (expiry == null)
                _memory.Set(key, value);
            else
                _memory.Set(key, value, absoluteExpirationRelativeToNow: (TimeSpan)expiry);
        }

        public Task<T> GetORrCreate<T>(string key, Func<Task<T>> func, TimeSpan? expiry = default)
        {
            return _memory.GetOrCreateAsync<T>(key, (ce) =>
            {
                var r = func();
                if (expiry != null)
                    ce.AbsoluteExpirationRelativeToNow = expiry;
                return r;
            });
        }
    }
}
