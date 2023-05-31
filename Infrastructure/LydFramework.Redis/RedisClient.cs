using LydFramework.Domain;
using Microsoft.Extensions.Configuration;
using StackExchange.Redis;
using Newtonsoft.Json;

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
        public async Task Set(string key,object value, TimeSpan expiry=default)
        {
            if(expiry==default)
                await _db.StringSetAsync(key, RedisValue.Unbox(value));
            else
                await _db.StringSetAsync(key, RedisValue.Unbox(value), expiry);
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

    }
}