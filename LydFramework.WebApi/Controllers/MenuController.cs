using AutoMapper;
using LydFramework.Application.Contracts.Menus;
using LydFramework.Application.Contracts.Menus.Dtos;
using LydFramework.Domain.Menus;
using LydFramework.Domain.Shared.Attributes;
using LydFramework.EFCore.Cores;
using LydFramework.EFCore.DbContexts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LydFramework.WebApi.Controllers
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
        public async Task<MenuDto> Create(AddMenuDto dto) => await _menuService.Create(dto);

        //获取树形菜单
        [HttpGet]
        [DisabledUnitOfWork]
        public async Task<List<MenuDto>> TreeMenu() => await _menuService.TreeMenu();

        //编辑菜单
        [HttpPut]
        public async Task<MenuDto> Update(UpdateMenuDto dto) => await _menuService.Update(dto);

    }
}
