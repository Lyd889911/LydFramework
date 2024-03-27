using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LydFramework.Common.BaseEntity 
{ 
    /// <summary>
    /// 是否有修改
    /// </summary>
    public interface IHasModify
    {
        /// <summary>
        /// 修改人
        /// </summary>
        long? ModifyBy { get; set; }
        /// <summary>
        /// 修改时间
        /// </summary>
        DateTime? ModifyTime { get; set; }
    }
}
