using AutoMapper;
using LydFramework.Application.Contracts.Menus.Dtos;
using LydFramework.Domain.Menus;
using LydFramework.Domain.Users;

namespace LydFramework.Application.AutoMapperProfiles
{
    public class MenuMapperProfile: Profile
    {
        public MenuMapperProfile()
        {
            CreateMap<Menu, MenuDto>();
            //CreateMap<List<Menu>, List<MenuDto>>();
        }
    }
}
