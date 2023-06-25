
using LydFramework.Application.Contracts.LydServers.Menus.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LydFramework.Application.Contracts.LydServers.Menus
{
    public interface IMenuService
    {
        /// <summary>
        /// 添加菜单
        /// </summary>
        public Task<MenuDto> Create(AddMenuDto dto);
        /// <summary>
        /// 树形菜单
        /// </summary>
        public Task<List<MenuDto>> TreeMenu();
        /// <summary>
        /// 更新菜单
        /// </summary>
        public Task<MenuDto> Update(UpdateMenuDto dto);
    }
}
