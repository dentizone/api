using Dentizone.Application.Interfaces;
using Dentizone.Domain.Enums;

namespace Dentizone.Infrastructure.Models
{
    internal class OrderStatus : IBaseEntity
    {
        public string Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public bool IsDeleted { get; set; }

        public int OrderId { get; set; }
        public virtual Order Order { get; set; }
        public OrderStatues Status { get; set; }

        public string? Comment { get; set; }




    }
}
