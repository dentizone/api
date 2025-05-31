using Dentizone.Domain.Enums;
using Dentizone.Domain.Interfaces;

namespace Dentizone.Domain.Entity
{
    public class OrderStatus : IBaseEntity
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public bool IsDeleted { get; set; }

        public string OrderId { get; set; }
        public virtual Order Order { get; set; }
        public OrderStatues Status { get; set; }

        public string? Comment { get; set; }
    }
}