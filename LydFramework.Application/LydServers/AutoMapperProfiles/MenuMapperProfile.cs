using AutoMapper;
using LydFramework.Application.Contracts.LydServers.Menus.Dtos;
using LydFramework.Domain.LydServers.Menus;

namespace LydFramework.Application.LydServers.AutoMapperProfiles
{
    public class MenuMapperProfile : Profile
    {
        public MenuMapperProfile()
        {
            CreateMap<Menu, MenuDto>();
            //CreateMap<List<Menu>, List<MenuDto>>();
        }
    }
}
