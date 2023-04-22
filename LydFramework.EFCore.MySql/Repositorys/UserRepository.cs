using LydFramework.EFCore.MySql.DbContexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LydFramework.EFCore.MySql.Repositorys
{
    public class UserRepository : IUserRepository
    {
        private readonly LydDbContext _dbContext;
        public UserRepository(LydDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<User> AddAsync(User user)
        {
            var result = await _dbContext.Users.AddAsync(user);
            return result.Entity;
        }

        public Task<User> FirstAsync(Expression<Func<User, bool>> predicate)
        {
            return _dbContext.Users.FirstAsync(predicate);
        }

        public IQueryable<User> ListAll()
        {
            return _dbContext.Users.AsQueryable();
        }

        public Task<List<User>> ListAsync(int index, int size)
        {
            return ListAll().Skip((index-1)*size).Take(size).ToListAsync();
        }

        public Task<int> TotalAsync()
        {
            return ListAll().CountAsync();
        }
    }
}
