﻿
using LydFramework.Application.Contracts.LydServers.Users;
using LydFramework.Application.Contracts.LydServers.Users.Dtos;
using LydFramework.EFCore.UnitOfWorks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LydFramework.WebApi.LydServers.Controllers
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
        [EFCoreUnitOfWork]
        //[Authorize(Roles = "管理员")]
        public async Task<UserDto> Create(AddUserDto dto) => await _userService.Create(dto);

        //修改用户信息
        [HttpPut]
        [EFCoreUnitOfWork]
        [Authorize(Roles = "管理员,普通用户")]
        public async Task<UserDto> Update(UpdateUsetDto dto) => await _userService.Update(dto);

        //更新用户角色
        [HttpPatch("role")]
        [EFCoreUnitOfWork]
        [Authorize(Roles = "管理员")]
        public async Task<UserDto> PatchRole(PatchRoleUserDto dto) => await _userService.PatchRole(dto);

        //更新用户状态
        [HttpPatch("status")]
        [EFCoreUnitOfWork]
        [Authorize(Roles = "管理员")]
        public async Task<UserDto> PatchStatus(PatchStatusUserDto dto) => await _userService.PatchStatus(dto);

        //删除用户
        [HttpDelete("{Id}")]
        [EFCoreUnitOfWork]
        [Authorize(Roles = "管理员")]
        public async Task Delete([FromRoute] long Id) => await _userService.Delete(Id);
    }
}
