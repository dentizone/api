using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dentizone.Domain.Entity;
using FluentValidation;

namespace Dentizone.Application.DTOs.Order
{
    public class OrderViewDTO
    {
        public string Id { get; set; }
        public string BuyerId { get; set; }
        public int totalAmount { get; set; }
        public DateTime createdAt { get; set; }
        public string currentStatus { get; set; }
        List<OrderItem> orderItems { get; set; } = new List<OrderItem>();
    }
    public class OrderViewDTOValidation: AbstractValidator<OrderViewDTO>
    {
        public OrderViewDTOValidation()
        {
            RuleFor(x => x.Id).NotEmpty().WithMessage("Order ID is required.");
            RuleFor(x => x.BuyerId).NotEmpty().WithMessage("Buyer ID is required.");
            RuleFor(x => x.totalAmount).GreaterThan(0).WithMessage("Total amount must be greater than zero.");
            RuleFor(x => x.createdAt).NotEmpty().WithMessage("Creation date is required.");
            RuleFor(x => x.currentStatus).NotEmpty().WithMessage("Current status is required.");
        }
    }
}
