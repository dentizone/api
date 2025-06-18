using Dentizone.Domain.Enums;

namespace Dentizone.Application.DTOs.PostDTO
{
    public class UpdatePostDto
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public PostItemCondition Condition { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public PostStatus Status { get; set; }
        public List<string> AssetIds { get; set; } = new();


        public DateTime? ExpireDate { get; set; }
    }
}