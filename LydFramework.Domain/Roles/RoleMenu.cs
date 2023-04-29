using LydFramework.Domain.Menus;
using LydFramework.Domain.Shared.BaseEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LydFramework.Domain.Roles
{
    public class RoleMenu:Entity
    {
        public long? MenuId { get; set; }
        public Menu Menu { get; set; }

        public long? RoleId { get; set; }
        public Role Role { get; set; }
        public RoleMenu(Role role,Menu menu)
        { 
            Menu = menu;
            MenuId=menu.Id;
            Role = role;
            RoleId = role.Id;
        }
        private RoleMenu()
        {

        }
    }
}
