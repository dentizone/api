using Dentizone.Application.Interfaces;
using Dentizone.Domain;
using Dentizone.Domain.Enums;

namespace Dentizone.Infrastructure.Models
{
    internal class UserAsset: IBaseEntity
    {
        public string Id { get; set; }

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
