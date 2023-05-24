using AutoMapper;
using LydFramework.Application.Contracts.Dtos;
using LydFramework.Application.Contracts.Users;
using LydFramework.Application.Contracts.Users.Dtos;
using LydFramework.Domain.Roles;
using LydFramework.Domain.Shared;
using LydFramework.Domain.Shared.Attributes;
using LydFramework.Domain.Users;
using LydFramework.EFCore.DbContexts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace LydFramework.WebApi.Controllers
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
        public async Task<ResultDto> Login(LoginDto dto)=> await _loginService.Login(dto);


    }
}
