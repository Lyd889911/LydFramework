namespace LydFramework.Application.Controllers.Users.Dtos
{
    public class UserDto
    {
        public string UserName { get; set; }
        public string Status { get; set; }
        public List<string> Roles { get; set; }
    }
}
