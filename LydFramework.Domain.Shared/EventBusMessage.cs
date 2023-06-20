using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LydFramework.Domain.Shared
{
    /// <summary>
    /// 发送事件的时候发送这个对象
    /// 基类
    /// </summary>
    public abstract class EventBusMessage
    {
        /// <summary>
        /// 发送消息的json字符串
        /// </summary>
        public string Message { get; set; }
        /// <summary>
        /// 发送消息的byte数据
        /// </summary>
        public byte[] Body { get; set; }
        public EventBusMessage(object msgData)
        {
            Message = JsonConvert.SerializeObject(msgData);
            Body = System.Text.Encoding.UTF8.GetBytes(Message);
        }
    }
}
