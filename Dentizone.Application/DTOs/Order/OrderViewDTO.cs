using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dentizone.Domain.Entity;
using FluentValidation;

namespace Dentizone.Application.DTOs.Order
{
    public class OrderViewDto
    {
        public string Id { get; set; }
        public string BuyerId { get; set; }
        public int TotalAmount { get; set; }
        public DateTime CreatedAt { get; set; }
        public string CurrentStatus { get; set; }
        List<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
    }

    public class OrderViewDtoValidation : AbstractValidator<OrderViewDto>
    {
        public OrderViewDtoValidation()
        {
            RuleFor(x => x.Id).NotEmpty().WithMessage("Order ID is required.");
            RuleFor(x => x.BuyerId).NotEmpty().WithMessage("Buyer ID is required.");
            RuleFor(x => x.TotalAmount).GreaterThan(0).WithMessage("Total amount must be greater than zero.");
            RuleFor(x => x.CreatedAt).NotEmpty().WithMessage("Creation date is required.");
            RuleFor(x => x.CurrentStatus).NotEmpty().WithMessage("Current status is required.");
        }
    }
}