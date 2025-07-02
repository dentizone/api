using Dentizone.Domain.Enums;

namespace Dentizone.Application.DTOs.Post
{
    public class UpdatePostDto
    {
        public required string Title { get; set; }
        public required string Description { get; set; }
        public decimal Price { get; set; }
        public PostItemCondition Condition { get; set; }
        public required string Street { get; set; }
        public required string City { get; set; }
        public List<string> AssetIds { get; set; } = new();

        public DateTime? ExpireDate { get; set; }
    }
}