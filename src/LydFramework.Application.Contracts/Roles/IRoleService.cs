using LydFramework.Application.Contracts.Dtos;
using LydFramework.Application.Contracts.Roles.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LydFramework.Application.Contracts.Roles
{
    public interface IRoleService
    {
        /// <summary>
        /// 添加角色
        /// </summary>
        public Task<RoleDto> Create(AddRoleDto dto);
        /// <summary>
        /// 更新角色
        /// </summary>
        public Task<RoleDto> Update(UpdateRoleDto dto);
        /// <summary>
        /// 得到角色分页列表
        /// </summary>
        public Task<PageDto> List(ListRoleDto dto);
    }
}
