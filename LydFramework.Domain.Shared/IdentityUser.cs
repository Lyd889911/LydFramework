using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LydFramework.Domain.Shared
{
    /// <summary>
    /// 登录后的认证用户
    /// </summary>
    public class IdentityUser<TKey> where TKey : class
    {
        public AsyncLocal<TKey?> Id { get; set; } = new AsyncLocal<TKey?>();
        public AsyncLocal<string?> Name { get; set; } = new AsyncLocal<string?>();
        public AsyncLocal<List<string>?> Roles { get; set; } = new AsyncLocal<List<string>?>();
    }
}
