using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;

namespace Util
{
    public class RedisHelper
    {
        private static ConnectionMultiplexer redis;
        private static IDatabase db;
        private ILogger<RedisHelper> logger;
        private IConfiguration configuration;
        public RedisHelper(ILogger<RedisHelper> logger,IConfiguration configuration)
        {
            if (redis==null)
                redis= ConnectionMultiplexer.Connect(configuration.GetSection("RedisConn").Value);
            if(db==null)
                db=redis.GetDatabase(Convert.ToInt32(configuration.GetSection("RedisDb").Value));
            this.logger = logger;
            this.configuration = configuration;
        }
        /// <summary>
        /// 获取字符串
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public async Task<string> GetString(string key)
        {
            DateTime t1 = DateTime.Now;
            RedisValue rv = await db.StringGetAsync(key);
            string r = rv.ToString();
            DateTime t2 = DateTime.Now;
            logger.LogInformation($"查询redis耗时：{(t2-t1).TotalMilliseconds}");
            return r;
        }
        /// <summary>
        /// 获取json
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        public async Task<T> GetJson<T>(string key)
        {
            string r = await GetString(key);
            return JsonConvert.DeserializeObject<T>(r);
        }
        /// <summary>
        /// 设置字符串
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public void SetString(string key,string value)
        {
            db.StringSetAsync(key,value);
        }
        /// <summary>
        /// 设置字符串带有过期时间
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="expiry"></param>
        public void SetString(string key,string value,TimeSpan expiry)
        {
            db.StringSetAsync(key, value, expiry);
        }
        public void SetString<T>(string key,T t)
        {
            string json = JsonConvert.SerializeObject(t);
            SetString(key, json);
        }
        public void SetString<T>(string key, T t, TimeSpan expiry)
        {
            string json = JsonConvert.SerializeObject(t);
            SetString(key, json,expiry);
        }
        /// <summary>
        /// 数字字符串递增
        /// </summary>
        /// <param name="key"></param>
        public async Task<long> Increment(string key)
        {
            return await db.StringIncrementAsync(key);
        }
        /// <summary>
        /// 删除建
        /// </summary>
        /// <param name="key"></param>
        public void DelKey(string key)
        {
            db.KeyDeleteAsync(key);
        }
        /// <summary>
        /// 获取全部redis键
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public List<string> MatchKey(string key)
        {
            DateTime t1 = DateTime.Now;
            IEnumerable<RedisKey> value = redis.GetServer(redis.GetEndPoints()[0]).Keys(0, key);
            List<string> keys = new List<string>();
            foreach(var v in value)
                keys.Add(v.ToString());
            DateTime t2 = DateTime.Now;
            logger.LogInformation($"匹配redis键耗时：{(t2-t1).TotalMilliseconds}");
            return keys;
        }
        /// <summary>
        /// 得到散列数据
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public async Task<Dictionary<string, string>> GetHash(string key)
        {
            var hashall = await db.HashGetAllAsync(key);
            Dictionary<string, string> hash = new Dictionary<string, string>();
            foreach(var kv in hashall)
                hash.Add(kv.Name, kv.Value);
            return hash;
        }
        /// <summary>
        /// 设置hash值
        /// </summary>
        /// <param name="key"></param>
        /// <param name="fkey"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public async Task<bool> SetHash(string key,string fkey,string value)
        {
            return await db.HashSetAsync(key, fkey, value);
        }
        /// <summary>
        /// 得到哈希的长度
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public async Task<long> GetHashLength(string key)
        {
            long len = await db.HashLengthAsync(key);
            return len;
        }
        /// <summary>
        /// 设置一个list
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public async Task RightPushList(string key,string value)
        {
            db.ListRightPushAsync(key, value);
        }
        /// <summary>
        /// 吐出一个list数据
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public async Task<string> LeftPopList(string key)
        {
            return await db.ListLeftPopAsync(key);
        }
        /// <summary>
        /// 得到hash某个键的值
        /// </summary>
        /// <param name="key"></param>
        /// <param name="fkey"></param>
        /// <returns></returns>
        public async Task<string> GetHashValue(string key,string fkey)
        {
            return await db.HashGetAsync(key, fkey);
        }
        /// <summary>
        /// 得到全部的list
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public async Task<List<string>> GetList(string key)
        {
            var rlist = await db.ListRangeAsync(key);
            List<string> sqllist = new List<string>();
            foreach(var r in rlist)
            {
                sqllist.Add(r);
            }
            return sqllist;
        }

    }
}
