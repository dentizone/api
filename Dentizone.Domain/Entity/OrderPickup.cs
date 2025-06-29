using Dentizone.Domain.Interfaces;

namespace Dentizone.Domain.Entity
{
    public class OrderPickup : IBaseEntity, IDeletable, IUpdatable
    {
        public required string OrderId { get; set; }
        public required Order Order { get; set; }
        public required string Street { get; set; }
        public required string City { get; set; }
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public bool IsDeleted { get; set; }
    }
}