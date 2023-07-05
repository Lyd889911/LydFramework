using LydFramework.Domain;
using Microsoft.Extensions.Configuration;
using StackExchange.Redis;
using Newtonsoft.Json;
using System.Collections;
using Newtonsoft.Json.Linq;
using LydFramework.Domain.InfrastructureContracts;

namespace LydFramework.Redis
{
    public class RedisProvider:IRedisProvider
    {
        private readonly RedisConnectionFactory _redisFactory;
        public RedisProvider(RedisConnectionFactory redisFactory)
        {
            _redisFactory = redisFactory;
        }
        

        #region String
        public async Task<T> Get<T>(string key)
        {
            var value = await _redisFactory.RedisDb.StringGetAsync(key);
            if (IsBaseType<T>())
                return (T)Convert.ChangeType(value.ToString(), typeof(T));
            return JsonConvert.DeserializeObject<T>(value.ToString());
        }
        public async Task Set(string key, object value, TimeSpan? expiry = default)
        {
            string valueString = JsonConvert.SerializeObject(value);
            await _redisFactory.RedisDb.StringSetAsync(key, valueString, expiry);
        }
        #endregion

        #region List
        public async Task<List<T>> GetList<T>(string key)
        {
            var values = await  _redisFactory.RedisDb.ListRangeAsync(key);
            List<T> list = values.Select(v => JsonConvert.DeserializeObject<T>(v.ToString()))?.ToList();
            return list;
        }
        public async Task PushList(string key, List<object> values, CacheListDirection direction = CacheListDirection.After)
        {
            RedisValue[] redisValues = ConvertRedisValues(values);
            if(direction == CacheListDirection.Before)
                await  _redisFactory.RedisDb.ListLeftPushAsync(key, redisValues);
            else if(direction == CacheListDirection.After)
                await  _redisFactory.RedisDb.ListRightPushAsync(key, redisValues);
        }
        //List转换成RedisValue[]
        private RedisValue[] ConvertRedisValues(List<object> values)
        {
            RedisValue[] redisValues = new RedisValue[values.Count()];
            for (int i = 0; i < values.Count; i++)
            {
                string valueString = JsonConvert.SerializeObject(values[i]);
                redisValues[i] = valueString;
            }
            return redisValues;
        }
        public async Task<T> PopList<T>(string key, CacheListDirection direction = CacheListDirection.After)
        {
            RedisValue value = new RedisValue();
            if(direction == CacheListDirection.Before)
                value = await  _redisFactory.RedisDb.ListLeftPopAsync(key);
            else if(direction == CacheListDirection.After)
                value = await  _redisFactory.RedisDb.ListRightPopAsync(key);

            if (IsBaseType<T>())
                return (T)Convert.ChangeType(value.ToString(), typeof(T));
            return JsonConvert.DeserializeObject<T>(value.ToString());
        }
        public async Task<long> ListLength(string key)
        {
            return await  _redisFactory.RedisDb.ListLengthAsync(key);
        }
        #endregion

        #region Hash
        public async Task<Dictionary<K, V>> GetHash<K,V>(string key)
        {
            var redisHash = await  _redisFactory.RedisDb.HashGetAllAsync(key);
            Dictionary<K, V> hash = new Dictionary<K, V>();
            foreach (var kv in redisHash)
            {
                K k = default;
                V v = default;
                if (IsBaseType<K>())
                    k = (K)Convert.ChangeType(kv.ToString(), typeof(K));
                else
                    k = JsonConvert.DeserializeObject<K>(kv.Name.ToString());

                if (IsBaseType<V>())
                    v = (V)Convert.ChangeType(kv.ToString(), typeof(V));
                else
                    v = JsonConvert.DeserializeObject<V>(kv.Value.ToString());

                hash.Add(k,v);
            }   
            return hash;
        }
        public async Task SetHashValue(string key, string hashKey, object value)
        {
            string valueString = JsonConvert.SerializeObject(value);
            await  _redisFactory.RedisDb.HashSetAsync(key, hashKey, valueString);
        }
        public async Task<T> GetHashValue<T>(string key, string hashKey)
        {
            var value = await  _redisFactory.RedisDb.HashGetAsync(key, hashKey);
            if (IsBaseType<T>())
                return (T)Convert.ChangeType(value.ToString(), typeof(T));
            return JsonConvert.DeserializeObject<T>(value.ToString());
        }
        #endregion

        #region 其他
        public async Task Remove(string key)
        {
            await  _redisFactory.RedisDb.KeyDeleteAsync(key);
        }
        #endregion
        //泛型是否是几个基础类型
        private bool IsBaseType<T>()
        {
            bool stringBool = typeof(T) == typeof(string);
            bool boolBool = typeof(T) == typeof(bool);
            bool longBool = typeof(T) == typeof(long);
            bool byteBool = typeof(T) == typeof(byte);
            bool shortBool =typeof(T) == typeof(short);
            bool intBool = typeof(T) == typeof(int);
            bool doubleBool = typeof(T) == typeof(double);
            bool floatBool = typeof(T) == typeof(float);
            bool decimalBool = typeof(T) == typeof(decimal);
            bool objectBool = typeof(T) == typeof(object);
            return stringBool || boolBool || longBool || byteBool ||
                shortBool || intBool || doubleBool || floatBool || decimalBool || objectBool;
        }
    }
}