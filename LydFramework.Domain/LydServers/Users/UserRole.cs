
using LydFramework.Domain.LydServers.Roles;
using LydFramework.Domain.Shared.BaseEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LydFramework.Domain.LydServers.Users
{
    public class UserRole:Entity
    {
        public long? UserId { get; set; }
        public User User { get; set; }

        public long? RoleId { get; set; }
        public Role Role { get; set;}
        public UserRole(User user,Role role)
        {
            User = user;
            UserId = user.Id;
            Role = role;
            RoleId= role.Id;
        }
        private UserRole()
        {

        }
    }
}
