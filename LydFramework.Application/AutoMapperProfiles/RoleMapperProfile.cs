using AutoMapper;
using LydFramework.Application.Controllers.Roles.Dtos;
using LydFramework.Domain.Roles;

namespace LydFramework.Application.AutoMapperProfiles
{
    public class RoleMapperProfile:Profile
    {
        public RoleMapperProfile()
        {
            CreateMap<Role,RoleDto>()
                .ForMember(dest => dest.MenuCount, opt => opt.MapFrom(src => src.RoleMenus.Count()));
        }
    }
}
