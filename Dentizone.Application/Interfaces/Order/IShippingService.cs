using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dentizone.Domain.Enums;

namespace Dentizone.Application.Interfaces.Order
{
   public interface IShippingService
    {
       
         void UpdateItemShipmentStatusAsync(string orderItemId, ShipmentActivityStatus newStatus);
    }
}
