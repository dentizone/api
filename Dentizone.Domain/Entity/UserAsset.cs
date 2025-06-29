using Dentizone.Domain.Enums;
using Dentizone.Domain.Interfaces;

namespace Dentizone.Domain.Entity
{
    public class UserAsset : IBaseEntity, IDeletable
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();

        public UserAssetsType Type { get; set; }
        public DateTime CreatedAt { get; set; }
        public bool IsDeleted { get; set; }

        // Every UserAsset is associated with a User
        public virtual required AppUser User { get; set; }
        public required string UserId { get; set; }

        // Every UserAsset is associated with an Asset
        public virtual required Asset Asset { get; set; }
        public required string AssetId { get; set; }
    }
}