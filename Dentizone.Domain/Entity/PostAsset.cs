using Dentizone.Domain.Interfaces;

namespace Dentizone.Domain.Entity
{
       public class PostAsset : IBaseEntity, IDeletable, IUpdatable
       {
              public string Id { get; set; } = Guid.NewGuid().ToString();
              public string PostId { get; set; }
              public string AssetId { get; set; }
              public DateTime CreatedAt { get; set; }
              public DateTime UpdatedAt { get; set; }
              public bool IsDeleted { get; set; }
              public virtual Post Post { get; set; }
              public virtual Asset Asset { get; set; }
       }
}