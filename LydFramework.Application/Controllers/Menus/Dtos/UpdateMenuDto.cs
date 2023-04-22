namespace LydFramework.Application.Controllers.Menus.Dtos
{
    public class UpdateMenuDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Icon { get; set; }
        public string Path { get; set; }
        public int Level { get; set; }
        public Guid? ParentId { get; set; }
    }
}
