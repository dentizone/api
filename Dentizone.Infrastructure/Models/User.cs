using System.ComponentModel.DataAnnotations;
using Dentizone.Application.Interfaces;
using Dentizone.Domain.Enums;
using Dentizone.Infrastructure.Models;
using Microsoft.AspNetCore.Identity;

namespace Dentizone.Domain
{
    internal class AppUser : IdentityUser, IBaseEntity
    {
        public string FullName { get; set; }
        public int AcademicYear { get; set; }
        public long? NationalId { get; set; }
        public KycStatus KycStatus { get; set; }
        public UserState Status { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

       public Post post { get; set; }


        public virtual Wallet Wallet { get; set; }

        public string UniversityId { get; set; }
        public virtual University University { get; set; }

        public ICollection<UserAsset> UserAssets { get; set; } = new List<UserAsset>();
        public ICollection<UserActivity> UserActivities { get; set; } = new List<UserActivity>();
        public ICollection<Post> Posts { get; set; }
        public bool IsDeleted { get; set; }
        public WithdrawalRequest withdrawalRequest { get; set; }
    }
}

