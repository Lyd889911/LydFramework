using AutoMapper;
using LydFramework.Application.Contracts.Dtos;
using LydFramework.Application.Contracts.LydServers.Roles;
using LydFramework.Application.Contracts.LydServers.Roles.Dtos;
using LydFramework.Domain.LydServers.Menus;
using LydFramework.Domain.LydServers.Roles;
using LydFramework.Module.Dependencys;

namespace LydFramework.Application.LydServers.Services
{
    public class RoleService : IRoleService, IScopedDependency
    {
        private readonly IRoleRepository _roleRepository;
        private readonly IMenuRepository _menuRepository;
        private readonly IMapper _mapper;
        public RoleService(IRoleRepository roleRepository, IMenuRepository menuRepository, IMapper mapper)
        {
            _roleRepository = roleRepository;
            _menuRepository = menuRepository;
            _mapper = mapper;
        }
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

        public async Task<PageDto> List(ListRoleDto dto)
        {
            List<Role> roles = await _roleRepository.ListAsync(dto.Index, dto.Size);
            int total = await _roleRepository.TotalAsync();
            var list = _mapper.Map<List<RoleDto>>(roles);
            return new PageDto(total, list);
        }

        public async Task<RoleDto> Update(UpdateRoleDto dto)
        {
            var role = await _roleRepository.FirstAsync(x => x.Id == dto.Id);
            role.Name = dto.Name;
            if (dto.MenuIds != null)
            {
                var oldmenuids = role.RoleMenus.Select(x => x.MenuId).ToList();
                //新的权限列表和旧的交集，新旧都有，不变的权限
                var c = dto.MenuIds.Intersect(oldmenuids);
                //新的和交集算差集，新增权限
                var addmenuids = dto.MenuIds.Except(c).ToList();
                //旧的和交集算差集，移除权限
                var removemenuids = oldmenuids.Except(c).ToList();

                //新增
                foreach (var menuId in addmenuids)
                {
                    var menu = await _menuRepository.FirstAsync(x => x.Id == menuId);
                    var roleMenu = new RoleMenu(role, menu);
                    role.RoleMenus.Add(roleMenu);
                    await _roleRepository.AddMenuAsync(roleMenu);
                }
                //移除
                foreach (var menuId in removemenuids)
                {
                    var roleMenu = role.RoleMenus.First(x => x.MenuId == menuId);
                    role.RoleMenus.Remove(roleMenu);
                }

            }
            return _mapper.Map<RoleDto>(role);
        }
    }
}
