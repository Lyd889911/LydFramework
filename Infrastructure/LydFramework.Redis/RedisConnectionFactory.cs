using Microsoft.Extensions.Configuration;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LydFramework.Redis
{
    public class RedisConnectionFactory
    {
        private ConnectionMultiplexer _connection;
        private object _lock = new object();
        private IDatabase _db;
        private int _dbIndex;
        private string _connectionString;
        private string _password;
        public RedisConnectionFactory(IConfiguration configuration)
        {
            _dbIndex = Convert.ToInt32(configuration["Redis:DbIndex"]);
            _connectionString = configuration["Redis:Connection"];
            _password = configuration["Redis:Password"];
            _password = string.IsNullOrWhiteSpace(_password) ? null : _password;
        }
        public ConnectionMultiplexer RedisConnection
        {
            get
            {
                //如果已经连接实例，直接返回
                if (this._connection != null && this._connection.IsConnected)
                {
                    return this._connection;
                }
                //加锁，防止异步编程中，出现单例无效的问题
                lock (_lock)
                {
                    if (this._connection != null)
                    {
                        //释放redis连接
                        this._connection.Dispose();
                    }

                    var config = new ConfigurationOptions
                    {
                        AbortOnConnectFail = false,
                        AllowAdmin = true,
                        ConnectTimeout = 15000,//改成15s
                        SyncTimeout = 5000,
                        Password = _password,//Redis数据库密码
                        EndPoints = { _connectionString }// connectionString 为IP:Port 如”192.168.2.110:6379”
                    };
                    _connection = ConnectionMultiplexer.Connect(config);
                }
                return _connection;
            }
            
        }
        public IDatabase RedisDb
        {
            get
            {
                return RedisConnection.GetDatabase(_dbIndex);
            }
        }
    }
}
