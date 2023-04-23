namespace LydFramework.Application.Controllers.Roles.Dtos
{
    public class AddRoleDto
    {
        public string RoleName { get; set; }
        public List<Guid>? MenuIds { get; set; }
    }
}
