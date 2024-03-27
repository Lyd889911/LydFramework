using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LydFramework.Common.BaseEntity
{
    public abstract class AggregateRoot:IEntity,IHasDelete,IHasModify,IHasCreate
    {
        public bool IsDeleted { get; set; }
        public long? DeleteBy { get; set; }
        public DateTime? DeleteTime { get; set; }
        public long? CreateBy { get; set; }
        public DateTime? CreateTime { get; set; }
        public long? ModifyBy { get; set; }
        public DateTime? ModifyTime { get; set; }
    }

}
