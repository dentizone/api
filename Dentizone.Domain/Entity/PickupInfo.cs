using Dentizone.Domain.Interfaces;

namespace Dentizone.Domain.Entity
{
    public class PickupInfo : IBaseEntity, IUpdatable
    {
        public string Street { get; set; }
        public string City { get; set; }

        public string SellerId { get; set; }
        public AppUser Seller { get; set; }


        public virtual Post Post { get; set; }
        public string PostId { get; set; }

        public string Id { get; set; } = Guid.NewGuid().ToString();
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}