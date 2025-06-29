using Dentizone.Domain.Enums;
using Dentizone.Domain.Interfaces;

namespace Dentizone.Domain.Entity
{
    public class UserActivity : IBaseEntity
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string UserId { get; set; } = string.Empty;
        public string FingerprintToken { get; set; } = string.Empty;
        public string Device { get; set; } = string.Empty;
        public string UserAgent { get; set; } = string.Empty;
        public DateTime DetectedAt { get; set; }

        public required string IpAddress { get; set; }
        public UserActivities ActivityType { get; set; }

        public virtual AppUser User { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}