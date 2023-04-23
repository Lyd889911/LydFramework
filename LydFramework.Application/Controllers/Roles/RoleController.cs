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
        public async Task<string> Create(AddRoleDto dto)
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
            return "添加成功";
        }

        //更新角色
        [HttpPut]
        public async Task Update()
        {

        }

        //获取角色列表
        [HttpGet]
        public async Task List()
        {

        }
    }
}
