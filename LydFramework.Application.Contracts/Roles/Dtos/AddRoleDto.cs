namespace LydFramework.Application.Contracts.Roles.Dtos
{
    public class AddRoleDto
    {
        public string RoleName { get; set; }
        public List<long?>? MenuIds { get; set; }
    }
}
