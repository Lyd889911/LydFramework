using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LydFramework.Domain.Shared.BaseEntity
{
    /// <summary>
    /// 是否有创建
    /// </summary>
    public interface IHasCreate<TKey>
    {
        /// <summary>
        /// 创建者
        /// </summary>
        TKey? CreateBy { get; }
        /// <summary>
        /// 创建时间
        /// </summary>
        DateTime? CreateTime { get; }
    }
}
