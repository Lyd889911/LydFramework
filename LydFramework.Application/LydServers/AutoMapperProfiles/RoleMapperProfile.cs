using AutoMapper;
using LydFramework.Application.Contracts.LydServers.Roles.Dtos;
using LydFramework.Domain.LydServers.Roles;

namespace LydFramework.Application.LydServers.AutoMapperProfiles
{
    public class RoleMapperProfile : Profile
    {
        public RoleMapperProfile()
        {
            CreateMap<Role, RoleDto>()
                .ForMember(dest => dest.MenuIds, opt => opt.MapFrom(src => src.RoleMenus.Select(x => x.MenuId).ToList()));
        }
    }
}
