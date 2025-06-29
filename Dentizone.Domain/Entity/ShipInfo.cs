using Dentizone.Domain.Interfaces;

namespace Dentizone.Domain.Entity
{
    public class ShipInfo : IBaseEntity
    {
        public string Street { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public string OrderId { get; set; } = string.Empty;
        public virtual Order Order { get; set; } = new();
        public string UserId { get; set; } = string.Empty;
        public virtual AppUser User { get; set; } = new();


        public string Id { get; set; } = Guid.NewGuid().ToString();
        public DateTime CreatedAt { get; set; }
    }
}