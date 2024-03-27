using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LydFramework.Domain.InfrastructureContracts
{
    /// <summary>
    /// 缓存提供者，实现RedisProvider
    /// </summary>
    public interface IRedisProvider
    {
        /// <summary>
        /// 获取值
        /// </summary>
        public Task<T> Get<T>(string key);
        /// <summary>
        /// 设置值
        /// </summary>
        public Task Set(string key, object value, TimeSpan? expiry = default);
        /// <summary>
        /// 获取整个列表
        /// </summary>
        public Task<List<T>> GetList<T>(string key);
        /// <summary>
        /// 从某个方向插入列表
        /// </summary>
        public Task PushList(string key, List<object> values, CacheListDirection direction = CacheListDirection.After);
        /// <summary>
        /// 从列表某个方向获取值，并删除该值
        /// </summary>
        public Task<T> PopList<T>(string key, CacheListDirection direction = CacheListDirection.After);
        /// <summary>
        /// 列表长度
        /// </summary>
        public Task<long> ListLength(string key);
        /// <summary>
        /// 获取整个哈希/字典
        /// </summary>
        public Task<Dictionary<K, V>> GetHash<K,V>(string key);
        /// <summary>
        /// 设置某个具体哈希值
        /// </summary>
        public Task SetHashValue(string key, string hashKey, object value);
        /// <summary>
        /// 得到某个具体的哈希值
        /// </summary>
        public Task<T> GetHashValue<T>(string key, string hashKey);
        /// <summary>
        /// 移除键
        /// </summary>
        public Task Remove(string key);
    }

    /// <summary>
    /// 缓存List数据的方向
    /// </summary>
    public enum CacheListDirection
    {
        Before,
        After
    }
}
