using Dentizone.Domain.Enums;

namespace Dentizone.Application.DTOs.UserActivity
{
    public class ActivityFilterDto
    {
        public string? UserId { get; set; }
        public string? Device { get; set; }
        public string? IpAddress { get; set; }
        public UserActivities? ActivityType { get; set; }

        public DateTime? DetectedAfter { get; set; }
        public DateTime? DetectedBefore { get; set; }

        public string? SearchText { get; set; }

        public int PageNumber { get; set; } = 1;
    }
}