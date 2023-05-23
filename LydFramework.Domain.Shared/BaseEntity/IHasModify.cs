using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LydFramework.Domain.Shared.BaseEntity
{
    /// <summary>
    /// 是否有修改
    /// </summary>
    public interface IHasModify
    {
        /// <summary>
        /// 修改人
        /// </summary>
        long? ModifyBy { get; }
        /// <summary>
        /// 修改时间
        /// </summary>
        DateTime? ModifyTime { get; }
    }
}
