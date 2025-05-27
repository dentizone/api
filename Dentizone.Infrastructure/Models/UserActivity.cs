using Dentizone.Application.Interfaces;
using Dentizone.Domain;
using Dentizone.Domain.Enums;

namespace Dentizone.Infrastructure.Models
{
    internal class UserActivity: IBaseEntity
    {
        public string Id { get; set; }


        public string FingerprintToken { get; set; }
        public string Device { get; set; }
        public string UserAgent { get; set; }
        public DateTime DetectedAt { get; set; }
        public UserActivities ActivityType { get; set; }
        public string IpAddress { get; set; }


        public virtual AppUser User { get; set; }
        public string UserId { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public bool IsDeleted { get; set; }
    }
}
