namespace LydFramework.Application.Controllers.Roles.Dtos
{
    public class UpdateRoleDto
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public List<Guid>? MenuIds { get; set; }
    }
}
