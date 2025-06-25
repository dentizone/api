using Dentizone.Domain.Enums;

namespace Dentizone.Application.DTOs.Order
{
    public class FilterOrderDto
    {
        public string? OrderId { get; set; }
        public string? BuyerName { get; set; }

        public OrderStatues? Status { get; set; }
    }
}