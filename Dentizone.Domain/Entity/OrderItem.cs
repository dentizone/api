using Dentizone.Application.Interfaces;

namespace Dentizone.Infrastructure.Models
{
    internal class OrderItem : IBaseEntity
    {
        public string Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public bool IsDeleted { get; set; }

        public string PostId { get; set; }
        public string OrderId { get; set; }
        public virtual Order Order { get; set; }

    }
}
