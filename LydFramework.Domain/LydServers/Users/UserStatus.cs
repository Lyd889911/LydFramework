using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LydFramework.Domain.LydServers.Users
{
    public enum UserStatus
    {
        /// <summary>
        /// 正常
        /// </summary>
        [Description("正常")]
        Normal = 0,

        /// <summary>
        /// 禁用
        /// </summary>
        [Description("禁用")]
        Forbidden = 1,
    }
}
