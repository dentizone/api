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
        public bool IsDeleted { get; set; }



        public virtual Wallet Wallet { get; set; }

        public string UniversityId { get; set; }
        public virtual University University { get; set; }
        public virtual ICollection<Favourite> Favourites { get; set; }

        public ICollection<UserAsset> UserAssets { get; set; } = new List<UserAsset>();
        public ICollection<UserActivity> UserActivities { get; set; } = new List<UserActivity>();
        public ICollection<Post> Posts { get; set; } = new List<Post>();
        public bool IsDeleted { get; set; }

        public virtual ICollection<Question> Questions { get; set; } = new List<Question>();

        public virtual ICollection<ShipInfo> ShippingAddresses { get; set; } = new List<ShipInfo>();
        public virtual ICollection<PickupInfo> PickupInfos { get; set; } = new List<PickupInfo>();
        public virtual ICollection<Cart> Carts { get; set; } = new List<Cart>();
        public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
        public virtual ICollection<Payment> Payments { get; set; } = new List<Payment>();

        public virtual ICollection<ReviewUx> UXReviews { get; set; } = new List<ReviewUx>();


       public virtual ICollection<Review> Reviews { get; set; } = new List<Review>();
        
    }
}

