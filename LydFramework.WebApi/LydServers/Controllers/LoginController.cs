using LydFramework.Application.Contracts.Dtos;
using LydFramework.Application.Contracts.LydServers.Users;
using LydFramework.Application.Contracts.LydServers.Users.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace LydFramework.WebApi.LydServers.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly ILoginService _loginService;
        public LoginController(ILoginService loginService)
        {
            _loginService = loginService;
        }

        [HttpPost]
        public async Task<ResultDto> Login(LoginDto dto) => await _loginService.Login(dto);


    }
}
