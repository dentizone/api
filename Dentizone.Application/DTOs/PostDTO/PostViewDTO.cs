using Dentizone.Application.DTOs.User;

namespace Dentizone.Application.DTOs.PostDTO
{
    public class PostViewDto
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public DateTime? ExpireDate { get; set; }
        public string Condition { get; set; }
        public string Status { get; set; }
        public UserView Seller { get; set; }
        public List<PostAssetView> Assets { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}