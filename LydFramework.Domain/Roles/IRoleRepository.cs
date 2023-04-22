using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LydFramework.Domain.Roles
{
    public interface IRoleRepository
    {
        IQueryable<Role> ListAll();

        Task<List<Role>> ListAsync(int index,int size);

        Task<int> TotalAsync();

        Task<Role> AddAsync(Role role);
    }
}
