using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dentizone.Domain;
using Dentizone.Domain.Enums;

namespace Dentizone.Infrastructure.Models
{
    internal class Order
    {
        public string id {  get; set; }
        public string buyer_id { get; set; }
        public AppUser user { get; set; }
        public OrderStatus status { get; set; }
        public DateTime placed_at { get; set; }
        
        public decimal commission_amount { get; set; }
        public decimal total_amount { get; set; }
        public DateTime completed_at { get; set; }
        public DateTime updated_at { get; set; }
        public ShipInfo ShipInfo { get; set; }

        public ICollection<PickupInfo> PickupInfos { get; set; }
        public ICollection<OrderPickup> OrderPickups { get; set; }
        public ICollection<OrderItem> OrderItems { get; set; }
    }
}
