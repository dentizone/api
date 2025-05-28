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
        public string WalletId { get; set; }
        public string ActivityType { get; set; }
        public decimal Amount { get; set; }
        public string ReferenceType { get; set; }
        public string ReferenceId { get; set; }
        public string Description { get; set; }


        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public bool IsDeleted { get; set; }
        public Wallet Wallet { get; set; }
    }
}
