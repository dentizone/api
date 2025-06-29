namespace Dentizone.Application.DTOs.Post.PostFilterDto
{
    public class SidebarFilterDto
    {
        public List<string> Cities { get; set; } = new();
        public List<CategoryFilterDto> Categories { get; set; } = new();
        public decimal MinPrice { get; set; }
        public decimal MaxPrice { get; set; }
    }
}