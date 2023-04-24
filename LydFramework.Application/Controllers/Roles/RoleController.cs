using AutoMapper;
using LydFramework.Application.Controllers.Roles.Dtos;
using LydFramework.Domain.Menus;
using LydFramework.Domain.Roles;
using LydFramework.Domain.Shared.Attributes;
using LydFramework.EFCore.MySql.DbContexts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LydFramework.Application.Controllers.Roles
{
    [Route("[controller]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private readonly IRoleRepository _roleRepository;
        private readonly IMenuRepository _menuRepository;
        private readonly IMapper _mapper;
        public RoleController(IRoleRepository roleRepository, IMenuRepository menuRepository, IMapper mapper)
        {
            _roleRepository = roleRepository;
            _menuRepository = menuRepository;
            _mapper = mapper;
        }

        //添加角色
        [HttpPost]
        [UnitOfWork(typeof(LydDbContext))]
        public async Task<RoleDto> Create(AddRoleDto dto)
        {
            List<Menu> menuList = new List<Menu>();
            Role role = new Role(dto.RoleName);
            if (dto.MenuIds != null)
            {
                foreach (var menuId in dto.MenuIds)
                {
                    Menu menu = await _menuRepository.FirstAsync(x => x.Id == menuId);
                    RoleMenu roleMenu = new RoleMenu(role, menu);
                    role.RoleMenus.Add(roleMenu);
                }
            }
            role = await _roleRepository.AddAsync(role);
            return _mapper.Map<RoleDto>(role);
        }

        //更新角色
        [HttpPut]
        public async Task Update(UpdateRoleDto dto)
        {
            var role = await _roleRepository.FirstAsync(x=>x.Id == dto.Id);
            role.Name = dto.Name;
            if(dto.MenuIds != null)
            {
                foreach (var mid in dto.MenuIds)
                {
                    var roleMenu = role.RoleMenus.FirstOrDefault(x => x.MenuId == mid);
                    if(roleMenu != null)
                    {

                    }
                }
            }


        }

        //获取角色列表
        [HttpGet]
        public async Task List()
        {

        }
    }
}
