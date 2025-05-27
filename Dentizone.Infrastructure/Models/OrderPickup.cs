using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dentizone.Infrastructure.Models
{
    internal class OrderPickup
    {
        public string id {  get; set; }
        public string order_id { get; set; }
        public Order order { get; set; }
        public string pickup_id { get; set; }
        public PickupInfo pickup { get; set; }
    }
}
