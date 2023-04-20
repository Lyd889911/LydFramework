using LydFramework.Domain.Menus;
using LydFramework.Domain.Shared.BaseEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LydFramework.Domain.Roles
{
    public class RoleMenu:Entity<Guid>
    {
        public Guid MenuId { get; set; }
        public Menu Menu { get; set; }

        public Guid RoleId { get; set; }
        public Role Role { get; set; }
        public RoleMenu(Role role,Menu menu):base(Guid.NewGuid())
        { 
            Menu = menu;
            MenuId=menu.Id;
            Role = role;
            RoleId = role.Id;
        }
    }
}
