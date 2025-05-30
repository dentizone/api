using Dentizone.Domain.Enums;
using Dentizone.Domain.Interfaces;

namespace Dentizone.Domain.Entity
{
    public class Asset : IBaseEntity
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string Url { get; set; }
        public long Size { get; set; }
        public AssetType Type { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public AssetStatus Status { get; set; }
        public bool IsDeleted { get; set; }
        public virtual ICollection<UserAsset> UserAssets { get; set; } = new List<UserAsset>();
        public virtual ICollection<PostAsset> PostAssets { get; set; } = new List<PostAsset>();
    }
}