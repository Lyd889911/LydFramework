using LydFramework.Domain.Shared.BaseRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace LydFramework.Domain.Menus
{
    public interface IMenuRepository
    {
        /// <summary>
        /// 得到全部菜单
        /// </summary>
        Task<List<Menu>> GetAllAsync();
        /// <summary>
        /// 添加菜单
        /// </summary>
        Task<Menu> AddAsync(Menu menu);
        /// <summary>
        /// 根据条件查找第一个菜单
        /// </summary>
        Task<Menu?> FirstAsync(Expression<Func<Menu, bool>> predicate);
    }
}
