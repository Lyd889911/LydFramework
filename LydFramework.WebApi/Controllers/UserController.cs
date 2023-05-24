using AutoMapper;
using LydFramework.Application.Contracts.Users;
using LydFramework.Application.Contracts.Users.Dtos;
using LydFramework.Domain.Roles;
using LydFramework.Domain.Shared.Attributes;
using LydFramework.Domain.Users;
using LydFramework.EFCore.DbContexts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Security.Claims;

namespace LydFramework.WebApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        //添加用户
        [HttpPost]
        //[Authorize(Roles = "管理员")]
        public async Task<UserDto> Create(AddUserDto dto) => await _userService.Create(dto);

        //修改用户信息
        [HttpPut]
        [Authorize(Roles = "管理员,普通用户")]
        public async Task<UserDto> Update(UpdateUsetDto dto) => await _userService.Update(dto);

        //更新用户角色
        [HttpPatch("role")]
        [Authorize(Roles = "管理员")]
        public async Task<UserDto> PatchRole(PatchRoleUserDto dto) => await _userService.PatchRole(dto);

        //更新用户状态
        [HttpPatch("status")]
        [Authorize(Roles = "管理员")]
        public async Task<UserDto> PatchStatus(PatchStatusUserDto dto) => await _userService.PatchStatus(dto);

        //删除用户
        [HttpDelete("{Id}")]
        [Authorize(Roles = "管理员")]
        public async Task Delete([FromRoute] long Id) => await _userService.Delete(Id);
    }
}
