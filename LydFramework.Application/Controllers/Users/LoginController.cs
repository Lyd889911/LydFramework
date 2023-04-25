using AutoMapper;
using LydFramework.Application.Controllers.Users.Dtos;
using LydFramework.Domain.Users;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LydFramework.Application.Controllers.Users
{
    [Route("[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        public LoginController(IUserRepository userRepository, IMapper mapper)
        {
            _mapper = mapper;
            _userRepository = userRepository;
        }

        [HttpPost]
        public async Task Login(LoginDto dto)
        {
            var user = _userRepository.FirstAsync(x => x.UserName == dto.Account);

        }
    }
}
