namespace LydFramework.Application.Contracts.LydServers.Menus.Dtos
{
    public class UpdateMenuDto
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public string Icon { get; set; }
        public string Path { get; set; }
        public int Level { get; set; }
        public long? ParentId { get; set; }
    }
}
