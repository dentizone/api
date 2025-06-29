using Dentizone.Domain.Enums;
using Dentizone.Domain.Interfaces;

namespace Dentizone.Domain.Entity
{
    public class Asset : IBaseEntity, IUpdatable, IDeletable
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string Url { get; set; } = string.Empty;
        public long Size { get; set; }
        public string UserId { get; set; } = string.Empty;

        public AssetType Type { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public AssetStatus Status { get; set; }
        public bool IsDeleted { get; set; }
        public virtual ICollection<UserAsset> UserAssets { get; set; } = new List<UserAsset>();
        public virtual ICollection<PostAsset> PostAssets { get; set; } = new List<PostAsset>();
        public virtual AppUser User { get; set; } = new();
    }
}