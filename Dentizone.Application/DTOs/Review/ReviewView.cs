using Dentizone.Application.DTOs.Order;
using Dentizone.Application.DTOs.User;

namespace Dentizone.Application.DTOs.Review
{
    public class ReviewView : ReviewDto
    {
        public required string Id { get; set; }
        public required UserView User { get; set; }

        public required OrderViewDto OrderViewDto { get; set; }

        public required string Sentiment { get; set; }
        public required DateTime CreatedAt { get; set; }
    }
}