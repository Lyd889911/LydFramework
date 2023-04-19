using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LydFramework.Domain.Shared.Base
{
    /// <summary>
    /// 是否有删除
    /// </summary>
    public interface IHasDelete<TKey>
    {
        /// <summary>
        /// 是否删除
        /// </summary>
        bool IsDeleted { get; }
        /// <summary>
        /// 删除人Id
        /// </summary>
        TKey? DeleteBy { get; }

        /// <summary>
        /// 删除时间
        /// </summary>
        DateTime? DeleteTime { get; }
    }
}
