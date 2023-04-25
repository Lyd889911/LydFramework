using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
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
        Task<RoleMenu> AddMenuAsync(RoleMenu roleMenu);
        Task<Role> FirstAsync(Expression<Func<Role, bool>> predicate);
    }
}
