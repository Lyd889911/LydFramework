using LydFramework.Domain;
using Microsoft.Extensions.Configuration;
using StackExchange.Redis;
using Newtonsoft.Json;
using System.Collections;

namespace LydFramework.Redis
{
    public class RedisClient:ICacheClient
    {
        private readonly ConnectionMultiplexer _redis;
        private readonly IDatabase _db;
        public RedisClient(IConfiguration configuration)
        {
            _redis = ConnectionMultiplexer.Connect(configuration["RedisConnection"]);
            _db = _redis.GetDatabase(Convert.ToInt32(configuration["RedisDbIndex"]));
        }

        public async Task<T> Get<T>(string key)
        {
            var rv = await _db.StringGetAsync(key);
            return JsonConvert.DeserializeObject<T>(rv.ToString())!;
        }
        public async Task Set(string key,object value, TimeSpan expiry=default, CacheDataType dataType = CacheDataType.String)
        {
            if (expiry == default)
            {
                switch (dataType)
                {
                    case CacheDataType.String: await _db.StringSetAsync(key, RedisValue.Unbox(value));break;
                    case CacheDataType.List: await _db.ListRightPushAsync(key, RedisValue.Unbox(value));break;
                }
            }
            else
            {
                await _db.StringSetAsync(key, RedisValue.Unbox(value), expiry);
            }
        }
        public async Task Remove(string key)
        {
            await _db.KeyDeleteAsync(key);
        }
        public async Task<Dictionary<object, object>> GetHash(string key)
        {
            var hashall = await _db.HashGetAllAsync(key);
            Dictionary<object, object> hash = new Dictionary<object, object>();
            foreach (var kv in hashall)
                hash.Add(kv.Name.Box()!, kv.Value.Box()!);
            return hash;
        }
        public async Task<bool> SetHash(string key, string fkey, object value)
        {
            return await _db.HashSetAsync(key, fkey, RedisValue.Unbox(value));
        }
        public async Task<object?> GetHashValue(string key, string fkey)
        {
            var rv = await _db.HashGetAsync(key, fkey);
            return rv.Box();
        }
        public async Task<List<object>> GetList(string key)
        {
            var rlist = await _db.ListRangeAsync(key);
            List<object> sqllist = new List<object>();
            foreach (var r in rlist)
            {
                sqllist.Add(r);
            }
            return sqllist;
        }

        public async Task<T> GetOrCreate<T>(string key, Func<T> func)
        {
            var t = await Get<T>(key);
            if (t != null)
                return t;
            var t2 = func();
            if(t2 == null)
                return t2;
            await Set(key, t2);
            return t2;
        }
    }
}