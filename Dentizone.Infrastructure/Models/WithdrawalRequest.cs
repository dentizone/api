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
    internal class WithdrawalRequest : IBaseEntity
    {
        public string Id { get; set; }
        public string wallet_id { get; set; }
        public decimal amount { get; set; }
        public WithdrawalRequestStatus status { get; set; } // e.g., Pending, Approved, Rejected
        public DateTime RequestedAt { get; set; }
        public DateTime ProcessedAt { get; set; }
        public decimal ProcessingFee { get; set; } // Optional: fee charged for processing the withdrawal


        public string admin_notes { get; set; }
        public AppUser user { get; set; } // The user who made the withdrawal request


        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public bool IsDeleted { get; set; }

        public Wallet Wallet { get; set; } // Navigation property to the Wallet entity
    }
}