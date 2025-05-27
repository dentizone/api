using Dentizone.Domain;
using Dentizone.Domain.Enums;

namespace Dentizone.Infrastructure.Models
{
       internal class Wallet
       {
              public string Id { get; set; }


              public decimal Balance { get; set; }
              public UserWallet Status { get; set; }
              public DateTime CreatedAt { get; set; }
              public DateTime UpdatedAt { get; set; }


              // Relationship: One Wallet to One User
              public virtual AppUser User { get; set; }
              public string UserId { get; set; }



       }
}
