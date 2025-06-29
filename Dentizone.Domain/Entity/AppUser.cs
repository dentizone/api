using Dentizone.Domain.Enums;
using Dentizone.Domain.Interfaces;

namespace Dentizone.Domain.Entity
{
    public class AppUser : IBaseEntity, IUpdatable, IDeletable
    {
        public string FullName { get; set; } = string.Empty;
        public string Username { get; set; } = string.Empty;
        public int AcademicYear { get; set; }
        public string Email { get; set; } = string.Empty;
        public long? NationalId { get; set; }
        public KycStatus KycStatus { get; set; }
        public UserState Status { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public bool IsDeleted { get; set; }
        public string UniversityId { get; set; } = string.Empty;
        public virtual University University { get; set; } = new();
        public Wallet Wallet { get; set; } = new();
        public ICollection<Favourite> Favourites { get; set; } = new List<Favourite>();
        public ICollection<UserAsset> UserAssets { get; set; }
        public ICollection<UserActivity> UserActivities { get; set; }
        public ICollection<Post> Posts { get; set; }
        public ICollection<Question> Questions { get; set; }
        public ICollection<ShipInfo> ShippingAddresses { get; set; }
        public ICollection<Cart> Carts { get; set; }
        public ICollection<Order> Orders { get; set; }
        public ICollection<Payment> Payments { get; set; }
        public ICollection<Review> Reviews { get; set; }
        public string Id { get; set; } = Guid.NewGuid().ToString();
    }
}