namespace LydFramework.Application.Contracts.LydServers.Roles.Dtos
{
    public class UpdateRoleDto
    {
        public long Id { get; set; }
        public string? Name { get; set; }
        public List<long?>? MenuIds { get; set; }
    }
}
