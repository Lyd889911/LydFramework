using AutoMapper;
using LydFramework.Application.Contracts.LydServers.Users.Dtos;
using LydFramework.Domain.LydServers.Users;

namespace LydFramework.Application.LydServers.AutoMapperProfiles
{
    public class UserMapperProfile : Profile
    {
        public UserMapperProfile()
        {
            CreateMap<User, UserDto>()
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status.ToString()))
                .ForMember(dest => dest.Roles, opt => opt.MapFrom(src => src.UserRoles.Select(x => x.RoleId)));
            CreateMap<User, LoginRDto>()
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status.ToString()));
        }
    }
}
