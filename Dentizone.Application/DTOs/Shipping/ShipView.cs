using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dentizone.Domain.Enums;

namespace Dentizone.Application.DTOs.Shipping
{
    public class ShipView
    {
        public string id { get; set; }
        public string ItemName { get; set; }

        public ShipmentActivityStatus ShipmentActivityStatus { get; set; }

        public DateTime Timestamp { get; set; }

        public string Comment { get; set; }
    }
}