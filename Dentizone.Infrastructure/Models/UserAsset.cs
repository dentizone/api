using Dentizone.Domain;
using Dentizone.Domain.Enums;

namespace Dentizone.Infrastructure.Models
{
       internal class UserAsset
       {
              public string Id { get; set; }

              public UserAssetsType Type { get; set; }
              public DateTime CreatedAt { get; set; }

              public virtual AppUser User { get; set; }
              public string UserId { get; set; }

              public virtual Asset Asset { get; set; }
              public string AssetId { get; set; }


       }
}
