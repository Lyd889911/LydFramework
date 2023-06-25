namespace LydFramework.Application.Contracts.LydServers.Menus.Dtos
{
    public class MenuDto
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public string Icon { get; set; }
        public string Path { get; set; }
        public int Level { get; set; }
        public long? ParentId { get; set; }
        public List<MenuDto>? ChildMenus { get; set; } = new List<MenuDto>();
    }
}
