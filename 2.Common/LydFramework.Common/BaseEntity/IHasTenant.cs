using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LydFramework.Common.BaseEntity
{
    public interface IHasTenant
    {
        /// <summary>
        /// 租户Id
        /// </summary>
        long? TenantId { get; set; }
    }
}
