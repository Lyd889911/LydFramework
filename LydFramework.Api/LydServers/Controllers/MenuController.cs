
using LydFramework.Application.Contracts.LydServers.Menus;
using LydFramework.Application.Contracts.LydServers.Menus.Dtos;
using LydFramework.EFCore.UnitOfWorks;
using Microsoft.AspNetCore.Mvc;

namespace LydFramework.WebApi.LydServers.Controllers
{
    [Route("[controller]")]
    [ApiController]
    //[Authorize(Roles = "管理员")]
    public class MenuController : ControllerBase
    {
        private readonly IMenuService _menuService;
        public MenuController(IMenuService menuService)
        {
            _menuService = menuService;
        }

        //创建一个菜单
        [HttpPost]
        [EFCoreUnitOfWork]
        public async Task<MenuDto> Create(AddMenuDto dto) => await _menuService.Create(dto);

        //获取树形菜单
        [HttpGet]
        public async Task<List<MenuDto>> TreeMenu() => await _menuService.TreeMenu();

        //编辑菜单
        [HttpPut]
        [EFCoreUnitOfWork]
        public async Task<MenuDto> Update(UpdateMenuDto dto) => await _menuService.Update(dto);

    }
}
