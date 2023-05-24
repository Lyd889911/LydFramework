using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LydFramework.Domain.Shared.BaseEntity
{
    public abstract class AggregateRoot:Entity,IHasCreate, IHasModify, IHasDelete
    {
        public bool IsDeleted { get; set; }
        public long? DeleteBy { get; set; }
        public DateTime? DeleteTime { get; set; }
        public long? CreateBy { get; set; }
        public DateTime? CreateTime { get; set; }
        public long? ModifyBy { get; set; }
        public DateTime? ModifyTime { get; set; }

        protected AggregateRoot()
        {
            CreateTime = DateTime.Now;
            ModifyTime = DateTime.Now;
        }
        public void Delete()
        {
            DeleteTime = DateTime.Now;
            IsDeleted = true;
        }
    }

}
