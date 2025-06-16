using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CloudinaryDotNet.Actions;
using Dentizone.Application.DTOs.Order;

namespace Dentizone.Application.Interfaces.Order
{
    public interface IOrderService
    {
        Task <string>CreateOrderAsync(CreateOrderDTO createOrderDTO, string buyerId);
        Task<OrderViewDTO> GetOrderByIdAsync(string orderId);
        Task<List<OrderViewDTO>> GetOrdersByBuyerAsync(string buyerId, int page);
        Task<List<OrderViewDTO>> CancelOrderAsync(string OrderId, string UserId);
    }
}
