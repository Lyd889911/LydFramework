using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LydFramework.Common.BaseEntity
{
    /// <summary>
    /// 是否有创建
    /// </summary>
    public interface IHasCreate
    {
        /// <summary>
        /// 创建者
        /// </summary>
        long? CreateBy { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        DateTime? CreateTime { get; set; }
    }
}
