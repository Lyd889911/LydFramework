using LydFramework.EFCore.MySql.DbContexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LydFramework.EFCore.MySql.Repositorys
{
    public class RoleRepository : IRoleRepository
    {
        private readonly LydDbContext _dbContext;
        public RoleRepository(LydDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<Role> AddAsync(Role role)
        {
            var result = await _dbContext.Roles.AddAsync(role);
            return result.Entity;
        }

        public IQueryable<Role> ListAll()
        {
            return _dbContext.Roles.AsQueryable<Role>();
        }

        public Task<List<Role>> ListAsync(int index, int size)
        {
            return ListAll().Skip((index - 1)*size).Take(size).ToListAsync();
        }

        public Task<int> TotalAsync()
        {
            return ListAll().CountAsync();
        }
    }
}
