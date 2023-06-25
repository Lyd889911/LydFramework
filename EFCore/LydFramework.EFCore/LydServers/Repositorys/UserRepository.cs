

namespace LydFramework.EFCore.LydServers.Repositorys
{
    public class UserRepository : IUserRepository, IScopedDependency
    {
        private readonly AuthDbContext _dbContext;
        public UserRepository(AuthDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<User> AddAsync(User user)
        {
            var result = await _dbContext.Users.AddAsync(user);
            return result.Entity;
        }

        public async Task<UserRole> AddRoleAsuync(UserRole userRole)
        {
            var result = await _dbContext.UserRoles.AddAsync(userRole);
            return result.Entity;
        }

        public async Task<User?> FirstAsync(Expression<Func<User, bool>> predicate)
        {
            var user = await _dbContext.Users
                .Include(x => x.UserRoles)
                .Include(x => x.UserAccessFail)
                .FirstOrDefaultAsync(predicate);
            return user;
        }

        public IQueryable<User> ListAll()
        {
            return _dbContext.Users.Include(x => x.UserRoles).AsQueryable();
        }

        public Task<List<User>> ListAsync(int index, int size)
        {
            return ListAll().Skip((index - 1) * size).Take(size).ToListAsync();
        }

        public Task<int> TotalAsync()
        {
            return ListAll().CountAsync();
        }
    }
}
