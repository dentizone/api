using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dentizone.Application.Interfaces;
using Dentizone.Domain;
using Dentizone.Domain.Enums;

namespace Dentizone.Infrastructure.Models
{
    internal class Payment: IBaseEntity
    {
        public string Id { get; set; }
        public string OrderId { get; set; }
        public string BuyerId { get; set; }
        public decimal Amount { get; set; }
        public PaymentMethod Method { get; set; } 
        public PaymentStatus Status { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public DateTime ProcessedAt { get; set; }
        public bool IsDeleted { get; set; }
        // Navigation properties
        public virtual AppUser Buyer { get; set; }
        public virtual Order Order { get; set; }

    }
}
