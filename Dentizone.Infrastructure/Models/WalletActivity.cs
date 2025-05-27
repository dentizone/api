using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dentizone.Application.Interfaces;

namespace Dentizone.Infrastructure.Models
{
    internal class WalletActivity : IBaseEntity
    {
        public string Id { get; set; }
        public string wallet_id { get; set; }
        public string activity_type { get; set; }
        public decimal amount { get; set; }
        public string reference_type { get; set; }
        public string reference_id { get; set; }
        public string description { get; set; }


        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public bool IsDeleted { get; set; }
        public Wallet Wallet { get; set; }
    }
}
