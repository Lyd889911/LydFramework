using AutoMapper;
using LydFramework.Application.Controllers.Menus.Dtos;
using LydFramework.Domain.Menus;
using LydFramework.Domain.Shared.Attributes;
using LydFramework.EFCore.MySql.DbContexts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LydFramework.Application.Controllers.Menus
{
    [Route("[controller]")]
    [ApiController]
    public class MenuController : ControllerBase
    {
        private readonly IMenuRepository _menuRepository;
        private readonly IMapper _mapper;
        public MenuController(IMenuRepository menuRepository,IMapper mapper)
        {
            _menuRepository = menuRepository;
            _mapper = mapper;
        }
        
        //创建一个菜单
        [HttpPost]
        [UnitOfWork(typeof(LydDbContext))]
        public async Task<MenuDto> Create(AddMenuDto dto)
        {
            Menu menu = new Menu(dto.Title, dto.Icon,dto.Path, dto.Level, dto.ParentId);
            menu = await _menuRepository.AddAsync(menu);
            return _mapper.Map<MenuDto>(menu);
        }

        //获取树形菜单
        [HttpGet]
        public async Task<List<MenuDto>> TreeMenu()
        {
            var list = await _menuRepository.GetAllAsync();
            List<MenuDto> treeMenu = _mapper.Map<List<MenuDto>>(list);
            foreach (var menuDto in treeMenu)
            {
                if (menuDto.ParentId != null)
                {
                    var parent = treeMenu.FirstOrDefault(x => x.ParentId == menuDto.Id);
                    if (parent != null)
                        parent.ChildMenus.Add(menuDto);
                }
            }
            treeMenu = treeMenu.Where(x => x.ParentId == null).ToList();
            return treeMenu;
        }

        //编辑菜单
        [HttpPut]
        [UnitOfWork(typeof(LydDbContext))]
        public async Task<MenuDto> Update(UpdateMenuDto dto)
        {
            var menu = await _menuRepository.FirstAsync(x => x.Id == dto.Id);
            menu.Update(dto.Title, dto.Icon, dto.Path, dto.Level, dto.ParentId);
            return _mapper.Map<MenuDto>(menu);
        }

    }
}
