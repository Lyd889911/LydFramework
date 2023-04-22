using LydFramework.Domain.Roles;
using LydFramework.Domain.Shared.BaseEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LydFramework.Domain.Users
{
    public class UserRole:Entity<Guid>
    {
        public Guid UserId { get; set; }
        public User User { get; set; }

        public Guid RoleId { get; set; }
        public Role Role { get; set;}
        public UserRole(User user,Role role):base(Guid.NewGuid())
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
