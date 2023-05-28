using AutoMapper;
using LydFramework.Application.Contracts.Menus;
using LydFramework.Application.Contracts.Menus.Dtos;
using LydFramework.Domain.Menus;
using LydFramework.Module.Dependencys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LydFramework.Application.Services
{
    public class MenuService : IMenuService, IScopedDependency
    {
        private readonly IMenuRepository _menuRepository;
        private readonly IMapper _mapper;
        public MenuService(IMenuRepository menuRepository, IMapper mapper)
        {
            _menuRepository = menuRepository;
            _mapper = mapper;
        }
        public async Task<MenuDto> Create(AddMenuDto dto)
        {
            Menu menu = new Menu(dto.Title, dto.Icon, dto.Path, dto.Level, dto.ParentId);
            menu = await _menuRepository.AddAsync(menu);
            return _mapper.Map<MenuDto>(menu);
        }

        public async Task<List<MenuDto>> TreeMenu()
        {
            var list = await _menuRepository.GetAllAsync();
            List<MenuDto> treeMenu = _mapper.Map<List<MenuDto>>(list);
            foreach (var menuDto in treeMenu)
            {
                if (menuDto.ParentId != null)
                {
                    var parent = treeMenu.FirstOrDefault(x => x.Id == menuDto.ParentId);
                    if (parent != null)
                        parent.ChildMenus.Add(menuDto);
                }
            }
            treeMenu = treeMenu.Where(x => x.ParentId == null).ToList();
            return treeMenu;
        }

        public async Task<MenuDto> Update(UpdateMenuDto dto)
        {
            var menu = await _menuRepository.FirstAsync(x => x.Id == dto.Id);
            menu.Update(dto.Title, dto.Icon, dto.Path, dto.Level, dto.ParentId);
            return _mapper.Map<MenuDto>(menu);
        }
    }
}
