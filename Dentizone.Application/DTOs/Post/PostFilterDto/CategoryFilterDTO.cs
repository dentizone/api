namespace Dentizone.Application.DTOs.Post.PostFilterDto
{
    public class CategoryFilterDto
    {
        public required string Id { get; set; }
        public required string CategoryName { get; set; }
        public List<string> Subcategories { get; set; } = new();
    }
}