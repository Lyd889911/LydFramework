using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace LydFramework.Domain.Shared
{
    public class IdentityProvider
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        public IdentityProvider(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        /// <summary>
        /// 用户id
        /// </summary>
        public string? UserId
        {
            get => _httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier);
        }
        /// <summary>
        /// 用户名
        /// </summary>
        public string? UserName
        {
            get => _httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.Name);
        }
        /// <summary>
        /// 租户id
        /// </summary>
        public string? Setid
        {
            get => _httpContextAccessor.HttpContext?.User.FindFirstValue("TenantId");
        }
    }
}
