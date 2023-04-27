using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LydFramework.Domain.Shared.BaseEntity
{
    public abstract class Entity<TKey> : IEntity<TKey>
    {
        protected Entity(TKey id) => Id = id;

        protected Entity()
        {
            #region 不同的主键生成策略
            if (typeof(TKey) == typeof(Guid))
            {
                Id = Guid.NewGuid() as TKey;
            }
            #endregion
        }

        public TKey Id { get; protected set; }
    }
}
