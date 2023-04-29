using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace LydFramework.Domain.Users
{
    public interface IUserRepository
    {
        IQueryable<User> ListAll();

        Task<List<User>> ListAsync(int index,int size);

        Task<int> TotalAsync();

        Task<User?> FirstAsync(Expression<Func<User, bool>> predicate);

        Task<User> AddAsync(User user);
        Task<UserRole> AddRoleAsuync(UserRole userRole);
    }
}
