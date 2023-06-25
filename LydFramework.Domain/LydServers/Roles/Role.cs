using LydFramework.Domain.LydServers.Users;
using LydFramework.Domain.Shared.BaseEntity;


namespace LydFramework.Domain.LydServers.Roles
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
