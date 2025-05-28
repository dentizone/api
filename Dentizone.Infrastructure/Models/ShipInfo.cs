using Dentizone.Application.Interfaces;
using Dentizone.Domain;

namespace Dentizone.Infrastructure.Models
{
    internal class ShipInfo : IBaseEntity
    {
        public string street { get; set; }
        public string city { get; set; }
        public string order_id { get; set; }
        public Order order { get; set; }
        public string user_id { get; set; }
        public AppUser user { get; set; }


        public string Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public bool IsDeleted { get; set; }
    }
}
