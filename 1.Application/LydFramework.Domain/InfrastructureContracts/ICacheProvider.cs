using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LydFramework.Domain.InfrastructureContracts
{
    public interface ICacheProvider
    {
        string CahceId { get; set; }
        T Get<T>(string key);
        void Set(string key, object value, TimeSpan? expiry = default);
        void Remove(string key);
        public Task<T> GetORrCreate<T>(string key, Func<Task<T>> func, TimeSpan? expiry = default);
        /// <summary>
        /// 设置某个具体哈希值
        /// </summary>
        public Task SetHashValue<T>(string key, string hashKey, T value);
        /// <summary>
        /// 得到某个具体的哈希值
        /// </summary>
        public Task<T> GetHashValue<T>(string key, string hashKey);
    }
}
