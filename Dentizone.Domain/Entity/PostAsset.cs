using Dentizone.Domain.Interfaces;

namespace Dentizone.Domain.Entity
{
    public class PostAsset : IBaseEntity
    {
        public string Id { get; set; }
        public string PostId { get; set; }
        public string AssetId { get; set; }
        public int DisplayOrder { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public bool IsDeleted { get; set; }
        public virtual Post Post { get; set; }
        public virtual Asset Asset { get; set; }
    }
}