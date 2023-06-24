using Microsoft.Extensions.Logging;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using RabbitMQ.Client.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace LydFramework.RabbitMQ
{
    public class RabbitMQConnection
    {
        private static IConnection _connection;
        private static IModel _channel;
        private bool _disposed;
        private ILogger<RabbitMQConnection> _logger;
        private IConnectionFactory _connectionFactory;
        private object _lock = new object();

        public RabbitMQConnection(IConnectionFactory connectionFactory, ILogger<RabbitMQConnection> logger)
        {
            _connectionFactory = connectionFactory;
            _logger = logger;
        }
        /// <summary>
        /// 是否已连接
        /// </summary>
        public bool IsConnected
        {
            get
            {
                return _connection != null && _connection.IsOpen;
            }
        }
        /// <summary>
        /// 通道是否打开
        /// </summary>
        public bool IsChannelOpened
        {
            get
            {
                return _channel != null && _channel.IsOpen;
            }
        }

        /// <summary>
        /// 创建Model
        /// </summary>
        /// <returns></returns>
        public IModel GetChannel()
        {
            if(IsChannelOpened)//打开就直接返回
                return _channel;
            else//没打开就创建
            {
                if (!IsConnected)
                    TryConnect();
                _channel = _connection.CreateModel();
                return _channel;
            }
        }

        /// <summary>
        /// 释放
        /// </summary>
        public void Dispose()
        {
            if (_disposed) return;

            _disposed = true;

            try
            {
                _connection.Dispose();
            }
            catch (IOException ex)
            {
                _logger.LogCritical(ex.ToString());
            }
        }

        /// <summary>
        /// 连接
        /// </summary>
        /// <returns></returns>
        public bool TryConnect()
        {
            _logger.LogInformation("RabbitMQ尝试连接...");

            lock (_lock)
            {
                try
                {
                    _connection = _connectionFactory.CreateConnection();
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex.Message);
                }

                if (IsConnected)
                {
                    _connection.ConnectionShutdown += OnConnectionShutdown;
                    _connection.CallbackException += OnCallbackException;
                    _connection.ConnectionBlocked += OnConnectionBlocked;

                    _logger.LogInformation("RabbitMQ连接成功！");

                    return true;
                }
                else
                {
                    _logger.LogError("RabbitMQ连接失败！");

                    return false;
                }

            }
        }

        /// <summary>
        /// 连接被阻断
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnConnectionBlocked(object sender, ConnectionBlockedEventArgs e)
        {

            _logger.LogWarning("RabbitMQ连接被阻断");
            TryConnect();
        }

        /// <summary>
        /// 连接出现异常
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void OnCallbackException(object sender, CallbackExceptionEventArgs e)
        {
            _logger.LogWarning("RabbitMQ连接异常");
            TryConnect();
        }

        /// <summary>
        /// 连接被关闭
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="reason"></param>
        void OnConnectionShutdown(object sender, ShutdownEventArgs reason)
        {
            _logger.LogWarning("RabbitMQ连接被关闭");
            TryConnect();
        }
    }
}
