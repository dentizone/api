using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dentizone.Domain.Enums;

namespace Dentizone.Infrastructure.Models
{
    internal class ShipmentActivity
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public string ShippedBy { get; set; }
        public ShipmentActivityStatus Status { get; set; }
        public string AssignedBy { get; set; }
        public string? ActivityDescription { get; set; }
        public DateTime CreatedAt { get; set; }

       
        public Order Order { get; set; }
    }
}
