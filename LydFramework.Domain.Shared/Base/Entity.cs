using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LydFramework.Domain.Shared.Base
{
    public abstract class Entity<TKey> : IEntity<TKey>
    {
        protected Entity(TKey id) => Id = id;

        protected Entity()
        {
        }

        public TKey Id { get; protected set; }
    }
}
