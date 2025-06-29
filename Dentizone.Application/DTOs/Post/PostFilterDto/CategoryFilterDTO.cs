namespace Dentizone.Application.DTOs.Post.PostFilterDto
{
    public class CategoryFilterDto
    {
        public string Id { get; set; }
        public string CategoryName { get; set; }
        public List<string> Subcategories { get; set; } = new List<string>();
    }
}