using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LydFramework.Domain.Shared.BaseEntity
{
    public abstract class AggregateRoot:Entity,IHasCreate, IHasModify, IHasDelete
    {
        public bool IsDeleted { get; protected set; }
        public long? DeleteBy { get; protected set; }
        public DateTime? DeleteTime { get; protected set; }
        public long? CreateBy { get; protected set; }
        public DateTime? CreateTime { get; protected set; }
        public long? ModifyBy { get; protected set; }
        public DateTime? ModifyTime { get; protected set; }

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
