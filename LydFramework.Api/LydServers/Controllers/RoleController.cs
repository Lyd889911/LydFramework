
using LydFramework.Application.Contracts.Dtos;
using LydFramework.Application.Contracts.LydServers.Roles;
using LydFramework.Application.Contracts.LydServers.Roles.Dtos;
using LydFramework.EFCore.UnitOfWorks;
using Microsoft.AspNetCore.Mvc;


namespace LydFramework.WebApi.LydServers.Controllers
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
        [EFCoreUnitOfWork]
        public async Task<RoleDto> Create(AddRoleDto dto) => await _roleService.Create(dto);

        //更新角色
        [HttpPut]
        [EFCoreUnitOfWork]
        public async Task<RoleDto> Update(UpdateRoleDto dto) => await _roleService.Update(dto);

        //获取角色列表
        [HttpGet("{Index}/{Size}")]
        public async Task<PageDto> List([FromRoute] ListRoleDto dto) => await _roleService.List(dto);
    }
}
