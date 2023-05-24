

using LydFramework.EFCore.DbContexts;

namespace LydFramework.EFCore.Repositorys
{
    public class MenuRepository : IMenuRepository
    {
        private readonly AuthDbContext _dbContext;
        public MenuRepository(AuthDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<Menu> AddAsync(Menu menu)
        {
            var result = await  _dbContext.Menus.AddAsync(menu);
            return result.Entity;
        }

        public async Task<Menu?> FirstAsync(Expression<Func<Menu, bool>> predicate)
        {
            var menu = await _dbContext.Menus.FirstOrDefaultAsync(predicate);
            return menu;
        }

        public Task<List<Menu>> GetAllAsync()
        {
            var list = _dbContext.Menus.ToListAsync();
            return list;
        }
    }
}
