using Dentizone.Domain.Enums;
using Dentizone.Domain.Interfaces;

namespace Dentizone.Domain.Entity
{
    public class UserAsset : IBaseEntity
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();

        public UserAssetsType Type { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public bool IsDeleted { get; set; }

        // Every UserAsset is associated with a User
        public virtual AppUser User { get; set; }
        public string UserId { get; set; }

        // Every UserAsset is associated with an Asset
        public virtual Asset Asset { get; set; }
        public string AssetId { get; set; }
    }
}