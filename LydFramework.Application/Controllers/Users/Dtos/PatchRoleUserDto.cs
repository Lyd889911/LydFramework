namespace LydFramework.Application.Controllers.Users.Dtos
{
    public class PatchRoleUserDto
    {
        public long Id { get; set; }
        public List<long?>? RoleIds { get; set; }
    }
}
