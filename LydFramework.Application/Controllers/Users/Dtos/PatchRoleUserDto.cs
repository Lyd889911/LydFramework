namespace LydFramework.Application.Controllers.Users.Dtos
{
    public class PatchRoleUserDto
    {
        public Guid Id { get; set; }
        public List<Guid>? RoleIds { get; set; }
    }
}
