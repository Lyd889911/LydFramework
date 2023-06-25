using LydFramework.Application.Contracts.LydServers.Users.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LydFramework.Application.Contracts.LydServers.Users
{
    public interface IUserService
    {
        /// <summary>
        /// 添加用户
        /// </summary>
        public Task<UserDto> Create(AddUserDto dto);
        /// <summary>
        /// 更新用户
        /// </summary>
        public Task<UserDto> Update(UpdateUsetDto dto);
        /// <summary>
        /// 修改用户角色
        /// </summary>
        public Task<UserDto> PatchRole(PatchRoleUserDto dto);
        /// <summary>
        /// 修改用户状态
        /// </summary>
        public Task<UserDto> PatchStatus(PatchStatusUserDto dto);
        /// <summary>
        /// 删除用户
        /// </summary>
        public Task Delete(long Id);
    }
}
