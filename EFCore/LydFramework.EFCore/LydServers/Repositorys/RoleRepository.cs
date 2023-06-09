﻿

namespace LydFramework.EFCore.LydServers.Repositorys
{
    public class RoleRepository : IRoleRepository, IScopedDependency
    {
        private readonly AuthDbContext _dbContext;
        public RoleRepository(AuthDbContext dbContext=null)
        {
            _dbContext = dbContext;
        }
        public async Task<Role> AddAsync(Role role)
        {
            var result = await _dbContext.Roles.AddAsync(role);
            return result.Entity;
        }

        public async Task<RoleMenu> AddMenuAsync(RoleMenu roleMenu)
        {
            var rm = await _dbContext.RoleMenus.AddAsync(roleMenu);
            return rm.Entity;
        }

        public async Task<Role?> FirstAsync(Expression<Func<Role, bool>> predicate)
        {
            var role = await _dbContext.Roles.Include(x => x.RoleMenus).FirstOrDefaultAsync(predicate);
            return role;
        }

        public IQueryable<Role> ListAll()
        {
            return _dbContext.Roles.Include(x => x.RoleMenus).AsQueryable<Role>();
        }

        public Task<List<Role>> ListAsync(int index, int size)
        {
            return ListAll().Skip((index - 1) * size).Take(size).ToListAsync();
        }

        public Task<int> TotalAsync()
        {
            return ListAll().CountAsync();
        }

    }
}
