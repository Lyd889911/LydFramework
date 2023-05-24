using AutoMapper;
using LydFramework.Application.Contracts.Dtos;
using LydFramework.Application.Contracts.Roles;
using LydFramework.Application.Contracts.Roles.Dtos;
using LydFramework.Domain.Menus;
using LydFramework.Domain.Roles;
using LydFramework.Domain.Shared.Attributes;
using LydFramework.EFCore.DbContexts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace LydFramework.WebApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    //[Authorize(Roles = "管理员")]
    public class RoleController : ControllerBase
    {
        private readonly IRoleService _roleService;
        public RoleController(IRoleService roleService)
        {
            _roleService = roleService;
        }

        //添加角色
        [HttpPost]
        [UnitOfWork(typeof(AuthDbContext))]
        public async Task<RoleDto> Create(AddRoleDto dto) => await _roleService.Create(dto);

        //更新角色
        [HttpPut]
        [UnitOfWork(typeof(AuthDbContext))]
        public async Task<RoleDto> Update(UpdateRoleDto dto) => await _roleService.Update(dto);

        //获取角色列表
        [HttpGet("{Index}/{Size}")]
        public async Task<PageDto> List([FromRoute] ListRoleDto dto) => await _roleService.List(dto);
    }
}
