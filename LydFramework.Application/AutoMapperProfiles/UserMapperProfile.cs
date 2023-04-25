using AutoMapper;
using LydFramework.Application.Controllers.Users.Dtos;
using LydFramework.Domain.Users;

namespace LydFramework.Application.AutoMapperProfiles
{
    public class UserMapperProfile:Profile
    {
        public UserMapperProfile()
        {
            CreateMap<User, UserDto>()
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status.ToString()))
                .ForMember(dest => dest.Roles, opt => opt.MapFrom(src => src.UserRoles.Select(x => x.RoleId)));
        }
    }
}
