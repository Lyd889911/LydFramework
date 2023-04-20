using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LydFramework.Domain.Shared.BaseEntity
{
    public interface ITenant
    {
        /// <summary>
        /// 租户Id
        /// </summary>
        Guid? TenantId { get; set; }
    }
}
