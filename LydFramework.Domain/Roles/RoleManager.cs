using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LydFramework.Domain.Roles
{
    public class RoleManager
    {
        private readonly IRoleRepository _roleRepository;
        public RoleManager(IRoleRepository roleRepository)
        {
            _roleRepository=roleRepository;
        }
    }
}
