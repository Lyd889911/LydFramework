using IdGen;
using LydFramework.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LydFramework.Domain.Shared.BaseEntity
{
    public abstract class Entity
    {
        protected Entity(long id) => Id = id;

        protected Entity()
        {
            Id = IdHelper.GetId();
        }

        public long Id { get; protected set; }
    }
}
