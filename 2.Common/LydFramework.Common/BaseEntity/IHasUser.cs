using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LydFramework.Common.BaseEntity
{
    public interface IHasUser
    {
        public long? UserId { get; set; }
    }
}
