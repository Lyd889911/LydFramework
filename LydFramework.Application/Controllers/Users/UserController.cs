using AutoMapper;
using LydFramework.Application.Controllers.Users.Dtos;
using LydFramework.Domain.Roles;
using LydFramework.Domain.Shared.Attributes;
using LydFramework.Domain.Users;
using LydFramework.EFCore.MySql.DbContexts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LydFramework.Application.Controllers.Users
{
    [Route("[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly IRoleRepository _roleRepository;
        private readonly IMapper _mapper;
        public UserController(IUserRepository userRepository, IRoleRepository roleRepository, IMapper mapper)
        {
            _mapper = mapper;
            _userRepository = userRepository;
            _roleRepository = roleRepository;
        }

        //添加用户
        [HttpPost]
        [UnitOfWork(typeof(LydDbContext))]
        public async Task<UserDto> Create(AddUserDto dto)
        {
            User user = new User(dto.UserName, dto.Password);
            if (dto.RoleIds != null)
            {
                foreach(var roleId in dto.RoleIds)
                {
                    Role role = await _roleRepository.FirstAsync(x => x.Id == roleId);
                    UserRole userRole = new UserRole(user, role);
                    user.UserRoles.Add(userRole);
                }
            }
            user = await _userRepository.AddAsync(user);
            var userDto = _mapper.Map<UserDto>(user);
            return userDto;
        }

        //修改用户信息
        [HttpPut]
        [UnitOfWork(typeof(LydDbContext))]
        public async Task<UserDto> Update(UpdateUsetDto dto)
        {
            var user = await _userRepository.FirstAsync(x => x.Id == dto.Id);
            user.UserName = dto.UserName;
            user.SetPassword(dto.Password);
            var userDto = _mapper.Map<UserDto>(user);
            return userDto;
        }

        //更新用户角色
        [HttpPatch]
        [UnitOfWork(typeof(LydDbContext))]
        public async Task<UserDto> PatchRole(PatchRoleUserDto dto)
        {
            var user = await _userRepository.FirstAsync(x => x.Id == dto.Id);
            if (dto.RoleIds != null)
            {
                var oldroleids = user.UserRoles.Select(x => x.RoleId).ToList();
                //新的权限列表和旧的交集，新旧都有，不变的权限
                var c = dto.RoleIds.Intersect(oldroleids);
                //新的和交集算差集，新增权限
                var addroleids = dto.RoleIds.Except(c).ToList();
                //旧的和交集算差集，移除权限
                var removeroleids = oldroleids.Except(c).ToList();

                //新增
                foreach (var roleId in addroleids)
                {
                    var role = await _roleRepository.FirstAsync(x => x.Id == roleId);
                    var userRole = new UserRole(user, role);
                    user.UserRoles.Add(userRole);
                    await _userRepository.AddRoleAsuync(userRole);
                }
                //移除
                foreach (var roleId in removeroleids)
                {
                    var userRole = user.UserRoles.First(x => x.RoleId == roleId);
                    user.UserRoles.Remove(userRole);
                }
            }
            var userDto = _mapper.Map<UserDto>(user);
            return userDto;
        }

        //更新用户状态
        [HttpPatch]
        [UnitOfWork(typeof(LydDbContext))]
        public async Task<UserDto> PatchStatus(PatchStatusUserDto dto)
        {
            var user = await _userRepository.FirstAsync(x => x.Id == dto.Id);
            user.Status = Enum.Parse<UserStatus>(dto.Status);
            var userDto = _mapper.Map<UserDto>(user);
            return userDto;
        }

        //删除用户
        [HttpDelete("{Id}")]
        [UnitOfWork(typeof(LydDbContext))]
        public async Task Delete([FromRoute]Guid Id)
        {
            var user = await _userRepository.FirstAsync(x => x.Id == Id);
            user.Delete();
        }
    }
}
