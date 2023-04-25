namespace LydFramework.Application.Controllers.Roles.Dtos
{
    public class RoleDto
    {
        public string? Name { get; set; }
        public List<Guid>? MenuIds { get; set; }
    }
}
