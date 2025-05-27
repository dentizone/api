using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dentizone.Infrastructure.Models
{
    internal class PickupInfo
    {
        public string id {  get; set; }
        public string street { get; set; }
        public string city { get; set; }
        public string post_id { get; set; }
        public Post Post { get; set; }
        public DateTime created_at { get; set; }
        public string order_id { get; set; }
        public Order Order { get; set; }

        public ICollection<OrderPickup> OrderPickups { get; set; }

    }
}
