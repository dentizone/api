using Dentizone.Application.Interfaces;
using Dentizone.Domain;

namespace Dentizone.Infrastructure.Models
{
    internal class PickupInfo : IBaseEntity
    {

        public string Street { get; set; }
        public string City { get; set; }

        public string SellerId { get; set; }
        public AppUser Seller { get; set; }


        public virtual Post Post { get; set; }
        public string PostId { get; set; }

        public string Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public bool IsDeleted { get; set; }
    }
}
