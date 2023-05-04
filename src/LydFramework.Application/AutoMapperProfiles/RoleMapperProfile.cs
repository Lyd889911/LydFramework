using AutoMapper;
using LydFramework.Application.Contracts.Roles.Dtos;
using LydFramework.Domain.Roles;

namespace LydFramework.Application.AutoMapperProfiles
{
    public class RoleMapperProfile:Profile
    {
        public RoleMapperProfile()
        {
            CreateMap<Role,RoleDto>()
                .ForMember(dest => dest.MenuIds, opt => opt.MapFrom(src => src.RoleMenus.Select(x=>x.MenuId).ToList()));
        }
    }
}
