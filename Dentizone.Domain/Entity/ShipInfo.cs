using Dentizone.Application.Interfaces;
using Dentizone.Domain;

namespace Dentizone.Infrastructure.Models
{
    internal class ShipInfo : IBaseEntity
    {
        public string Street { get; set; }
        public string City { get; set; }
        public string OrderId { get; set; }
        public virtual Order Order { get; set; }
        public string UserId { get; set; }
        public virtual AppUser User { get; set; }


        public string Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public bool IsDeleted { get; set; }
    }
}
