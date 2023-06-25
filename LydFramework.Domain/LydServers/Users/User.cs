
using LydFramework.Domain.Shared.BaseEntity;
using LydFramework.Tools;


namespace LydFramework.Domain.LydServers.Users
{
    public class User : AggregateRoot
    {
        public string UserName { get; set; }
        public string PasswordHash { get; set; }
        public UserStatus Status { get; set; }
        public UserAccessFail UserAccessFail { get; set; }
        public List<UserRole> UserRoles { get; set; }
        public User(string userName,string password)
        {
            UserName = userName;
            PasswordHash = HashHelper.ComputeMd5Hash(password);
            Status = UserStatus.Normal;
            UserAccessFail = new UserAccessFail(this);
            UserRoles = new List<UserRole>();
        }
        private User()
        {

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
        public void SetPassword(string password)
        {
            PasswordHash = HashHelper.ComputeMd5Hash(password);
        }
    }
}
