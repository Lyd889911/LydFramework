using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LydFramework.Common.BaseEntity
{
    /// <summary>
    /// 是否有删除
    /// </summary>
    public interface IHasDelete
    {
        /// <summary>
        /// 是否删除
        /// </summary>
        bool IsDeleted { get; set; }
        /// <summary>
        /// 删除人Id
        /// </summary>
        long? DeleteBy { get; set; }

        /// <summary>
        /// 删除时间
        /// </summary>
        DateTime? DeleteTime { get; set; }
    }
}
