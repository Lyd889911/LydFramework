using LydFramework.Domain.Shared.BaseEntity;
using LydFramework.Domain.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LydFramework.Domain.Roles
{
    public class Role:AggregateRoot
    {
        public string Name { get; set; }

        public List<UserRole> UserRoles { get; set; }
        public List<RoleMenu> RoleMenus { get; set; }
        public Role(string name)
        {
            Name = name;
            UserRoles = new List<UserRole>();
            RoleMenus = new List<RoleMenu>();
        }
        private Role()
        {

        }

    }
}
