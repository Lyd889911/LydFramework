namespace LydFramework.Application.Controllers.Users.Dtos
{
    public class AddUserDto
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public List<long?>? RoleIds { get; set; }
    }
}
