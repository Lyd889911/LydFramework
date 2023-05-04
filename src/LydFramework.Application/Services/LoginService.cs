using AutoMapper;
using LydFramework.Application.Contracts.Dtos;
using LydFramework.Application.Contracts.Users;
using LydFramework.Application.Contracts.Users.Dtos;
using LydFramework.Domain.Roles;
using LydFramework.Domain.Users;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace LydFramework.Application.Services
{
    public class LoginService : ILoginService
    {
        private readonly IUserRepository _userRepository;
        private readonly IRoleRepository _roleRepository;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;
        public LoginService(IUserRepository userRepository,
            IRoleRepository roleRepository,
            IMapper mapper,
            IConfiguration configuration)
        {
            _mapper = mapper;
            _userRepository = userRepository;
            _roleRepository = roleRepository;
            _configuration = configuration;
        }
        public async Task<ResultDto> Login(LoginDto dto)
        {
            User user = null;
            switch (dto.LoginType.ToLower())
            {
                case "username": user = await _userRepository.FirstAsync(x => x.UserName == dto.Account); break;
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
                loginRDto.accessToken = Jwt(user.Id.ToString(), user.UserName, loginRDto.Roles);
                #endregion

                return new ResultDto(200, "登陆成功", loginRDto);
            }
            else
            {
                user.UserAccessFail.Fail();
                return new ResultDto(400, "登陆失败");
            }
        }

        private string Jwt(string id,string username,List<string> roles)
        {
            //主体内容payload
            //可以自定义键值对，也可以使用ClaimTypes里面定义的一些东西
            var claims = new List<Claim>();
            claims.Add(new Claim(ClaimTypes.NameIdentifier, id));
            claims.Add(new Claim(ClaimTypes.Name, username));
            foreach (string role in roles)
            {
                //可以添加多个相同的Type
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            //密钥，保存在配置文件的
            string SecurityKey = _configuration["JwtSecurityKey"];
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(SecurityKey));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256); //使用签名算法

            //过期时间,一般授权jwt过期时间很短，只有几分钟，免得被人拿到了一直用
            var expires = DateTime.Now.AddMinutes(100);

            //这是创建token
            var token = new JwtSecurityToken(
                claims: claims,
                expires: expires,
                signingCredentials: credentials
            );
            //最终生成了token字符串
            var jwt = new JwtSecurityTokenHandler().WriteToken(token);
            return jwt;
        }
    }
}
