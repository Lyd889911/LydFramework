using LydFramework.Domain.Menus;
using LydFramework.Domain.Roles;
using LydFramework.Domain.Users;
using LydFramework.EFCore.DbContexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LydFramework.DbMigration
{
    public class SeedData
    {
        private readonly IRoleRepository _roleRepository;
        private readonly IUserRepository _userRepository;
        private readonly IMenuRepository _menuRepository;
        private readonly LydDbContext _dbContext;
        private readonly ILogger<SeedData> _logger;
        public SeedData(IRoleRepository roleRepository,
            IUserRepository userRepository,
            IMenuRepository menuRepository,
            LydDbContext dbContext,
            ILogger<SeedData> logger)
        {
            _roleRepository = roleRepository;
            _userRepository = userRepository;
            _menuRepository = menuRepository;
            _dbContext = dbContext;
            _logger = logger;

        }

        public async Task Seed()
        {
            _logger.LogInformation("开始生成种子数据");
            #region menus
            var m1 = new Menu("首页", "sy.jpg", "/home", 1, null);
            var m2 = new Menu("设置", "sz.jpg", "/setting", 1, null);
            var m3 = new Menu("我的", "wd.jpg", "/me", 1, null);
            var m4 = new Menu("系统管理", "xtgl.jpg", "/systems", 1, null);
            var m5 = new Menu("用户管理", "yhgl.jpg", "/users", 2, m4.Id);
            var m6 = new Menu("角色管理", "jsgl.jpg", "/roles", 2, m4.Id);
            var m7 = new Menu("菜单管理", "cdgl.jpg", "/menus", 2, m4.Id);
            List<Menu> mlist = new List<Menu>() { m1, m2, m3, m4, m5, m6, m7 };
            foreach (var menu in mlist)
            {
                var em = await _menuRepository.FirstAsync(x => x.Title == menu.Title);
                if (em == null)
                    await _menuRepository.AddAsync(menu);
            }
            #endregion

            #region roles
            Role r1 = new Role("admin");
            r1.RoleMenus.Add(new RoleMenu(r1, m1));
            r1.RoleMenus.Add(new RoleMenu(r1, m2));
            r1.RoleMenus.Add(new RoleMenu(r1, m3));
            r1.RoleMenus.Add(new RoleMenu(r1, m4));
            r1.RoleMenus.Add(new RoleMenu(r1, m5));
            r1.RoleMenus.Add(new RoleMenu(r1, m6));
            r1.RoleMenus.Add(new RoleMenu(r1, m7));
            Role r2 = new Role("common");
            r2.RoleMenus.Add(new RoleMenu(r2, m1));
            r2.RoleMenus.Add(new RoleMenu(r2, m2));
            r2.RoleMenus.Add(new RoleMenu(r2, m3));
            List<Role> rlist = new List<Role>() { r1, r2 };
            foreach (var role in rlist)
            {
                var er = await _roleRepository.FirstAsync(x => x.Name == role.Name);
                if (er == null)
                    await _roleRepository.AddAsync(role);
            }
            #endregion

            #region users
            User u1 = new User("aaa", "123456");
            u1.UserRoles.Add(new UserRole(u1, r1));
            User u2 = new User("ccc", "123456");
            u2.UserRoles.Add(new UserRole(u2, r2));
            List<User> ulist = new List<User>() { u1, u2 };
            foreach (var user in ulist)
            {
                var eu = await _userRepository.FirstAsync(x => x.UserName == user.UserName);
                if (eu == null)
                    await _userRepository.AddAsync(user);
            }
            #endregion

            _dbContext.SaveChanges();
            _logger.LogInformation("种子数据生成完毕");
        }
    }
}
