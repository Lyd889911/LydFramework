using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LydFramework.Domain.Shared.BaseEntity
{
    public abstract class AggregateRoot : Entity<Guid>, IHasCreate<Guid?>, IHasModify<Guid?>, IHasDelete<Guid?>
    {
        public bool IsDeleted { get; protected set; }
        public Guid? DeleteBy { get; protected set; }
        public DateTime? DeleteTime { get; protected set; }
        public Guid? CreateBy { get; protected set; }
        public DateTime? CreateTime { get; protected set; }
        public Guid? ModifyBy { get; protected set; }
        public DateTime? ModifyTime { get; protected set; }
        protected AggregateRoot() : base(Guid.NewGuid())
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

    public abstract class AggregateRoot<TKey> : Entity<TKey>, IHasCreate<TKey>, IHasModify<TKey>, IHasDelete<TKey>
    {
        public bool IsDeleted { get; protected set; }
        public TKey? DeleteBy { get; protected set; }
        public DateTime? DeleteTime { get; protected set; }
        public TKey? CreateBy { get; protected set; }
        public DateTime? CreateTime { get; protected set; }
        public TKey? ModifyBy { get; protected set; }
        public DateTime? ModifyTime { get; protected set; }

        protected AggregateRoot(TKey id):base(id)
        {
            CreateTime = DateTime.Now;
            ModifyTime = DateTime.Now;
        }
        protected AggregateRoot(TKey id ,TKey? createId):base(id)
        {
            CreateTime = DateTime.Now;
            ModifyTime = DateTime.Now;
            CreateBy = createId;
            ModifyBy = createId;
        }
    }

}
