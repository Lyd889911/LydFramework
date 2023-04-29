using AutoMapper;
using LydFramework.Application.Controllers.Users.Dtos;
using LydFramework.Application.Dtos;
using LydFramework.Domain.Roles;
using LydFramework.Domain.Shared;
using LydFramework.Domain.Shared.Attributes;
using LydFramework.Domain.Users;
using LydFramework.EFCore.MySql.DbContexts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace LydFramework.Application.Controllers.Users
{
    [Route("[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly IRoleRepository _roleRepository;
        private readonly IMapper _mapper;
        private readonly JWT _jwt;
        private readonly Domain.Shared.IdentityUser<long?> _identityUser;
        public LoginController(IUserRepository userRepository, 
            IRoleRepository roleRepository, 
            IMapper mapper,
            JWT jwt,
            Domain.Shared.IdentityUser<long?> identityUser)
        {
            _mapper = mapper;
            _userRepository = userRepository;
            _roleRepository = roleRepository;
            _jwt = jwt;
            _identityUser = identityUser;
        }

        [HttpPost]
        [UnitOfWork(typeof(LydDbContext))]
        public async Task<ResultDto> Login(LoginDto dto)
        {
            User user = null;
            switch (dto.LoginType.ToLower())
            {
                case "username": user = await _userRepository.FirstAsync(x => x.UserName == dto.Account);break;
                default: return new ResultDto(400, "登陆错误");
            }
            
            if (user.UserAccessFail.IsLockOut())
                return new ResultDto(400, "账号锁定无法登陆");
            bool b = user.CheckPassword(dto.Password);
            if (b)
            {
                var loginRDto = _mapper.Map<LoginRDto>(user);
                if (user.UserRoles != null)
                {
                    foreach (var userRole in user.UserRoles)
                    {
                        var role = await _roleRepository.FirstAsync(x => x.Id == userRole.RoleId);
                        loginRDto.Roles.Add(role.Name);
                    }
                }

                #region 生成jwt
                _identityUser.Id.Value = user.Id;
                _identityUser.Name.Value = user.UserName;
                _identityUser.Roles.Value = loginRDto.Roles;
                loginRDto.accessToken = _jwt.Create(_identityUser);
                #endregion

                return new ResultDto(200, "登陆成功", loginRDto);
            }
            else
            {
                user.UserAccessFail.Fail();
                return new ResultDto(400, "登陆失败");
            }
                
            
        }
    }
}
