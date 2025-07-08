using Dentizone.Application.DTOs.User;

namespace Dentizone.Application.DTOs.Post
{
    public class PostViewDto
    {
        public string Id { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public decimal Price { get; set; } = 0;
        public DateTime? ExpireDate { get; set; } = null;
        public string Condition { get; set; } = string.Empty;
        public string Category { get; set; } = string.Empty;
        public string SubCatgory { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
        public string Slug { get; set; } = string.Empty;
        public UserView Seller { get; set; } = new();
        public List<PostAssetView> Assets { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}