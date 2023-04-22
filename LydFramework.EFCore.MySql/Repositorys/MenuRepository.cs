

using LydFramework.EFCore.MySql.DbContexts;

namespace LydFramework.EFCore.MySql.Repositorys
{
    public class MenuRepository : IMenuRepository
    {
        private readonly LydDbContext _dbContext;
        public MenuRepository(LydDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<Menu> AddAsync(Menu menu)
        {
            var result = await  _dbContext.Menus.AddAsync(menu);
            return result.Entity;
        }

        public Task<Menu> FirstAsync(Expression<Func<Menu, bool>> predicate)
        {
            var menu = _dbContext.Menus.FirstAsync(predicate);
            return menu;
        }

        public Task<List<Menu>> GetAllAsync()
        {
            var list = _dbContext.Menus.ToListAsync();
            return list;
        }
    }
}
