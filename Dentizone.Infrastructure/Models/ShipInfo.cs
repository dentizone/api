using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dentizone.Domain;

namespace Dentizone.Infrastructure.Models
{
    internal class ShipInfo
    {
        public string id {  get; set; }
        public string street { get; set; }
        public string city { get; set; }
        public string order_id { get; set; }
        public Order order { get; set; }
        public string user_id { get; set; }
        public AppUser user { get; set; }
        public DateTime created_at { get; set; }
    }
}
