using LydFramework.Domain.Shared.BaseEntity;
using LydFramework.Domain.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LydFramework.Domain.Roles
{
    public class Role:AggregateRoot<Guid>
    {
        public string Name { get; set; }

        public ICollection<UserRole> UserRoles { get; set; }
        public ICollection<RoleMenu> RoleMenus { get; set; }
        public Role(string name):base(Guid.NewGuid())
        {
            Name = name;
        }

    }
}
