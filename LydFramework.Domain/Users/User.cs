using LydFramework.Domain.Roles;
using LydFramework.Domain.Shared.BaseEntity;
using LydFramework.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LydFramework.Domain.Users
{
    public class User : AggregateRoot<Guid>
    {
        public string UserName { get; set; }
        public string PasswordHash { get; set; }
        public UserStatus Status { get; set; }
        public UserAccessFail UserAccessFail { get; set; }
        public ICollection<UserRole> UserRoles { get; set; }
        public User(string userName,string password) : base(Guid.NewGuid())
        {
            UserName = userName;
            PasswordHash = HashHelper.ComputeMd5Hash(password);
            Status = UserStatus.Normal;
            UserAccessFail = new UserAccessFail(this);
        }
        public bool HasPassword()
        {
            return !string.IsNullOrEmpty(this.PasswordHash);
        }
        public void ChangePassword(string password)
        {
            if (password.Length <= 3)
                throw new ArgumentOutOfRangeException("密码长度必须大于三");
            this.PasswordHash = HashHelper.ComputeMd5Hash(password);
        }
        public bool CheckPassword(string password)
        {
            return this.PasswordHash == HashHelper.ComputeMd5Hash(password);
        }
    }
}
