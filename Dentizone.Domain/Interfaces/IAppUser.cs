using Dentizone.Domain.Entity;
using Dentizone.Domain.Enums;

namespace Dentizone.Domain.Interfaces
{
    public interface IAppUser
    {
        string Id { get; set; }
        string FullName { get; set; }

        string Username { get; set; }
        int AcademicYear { get; set; }
        long? NationalId { get; set; }
        KycStatus KycStatus { get; set; }
        UserState Status { get; set; }
        DateTime CreatedAt { get; set; }
        DateTime UpdatedAt { get; set; }
        bool IsDeleted { get; set; }

        string UniversityId { get; set; }
        University University { get; set; }
        Wallet Wallet { get; set; }

        ICollection<Favourite> Favourites { get; set; }
        ICollection<UserAsset> UserAssets { get; set; }
        ICollection<UserActivity> UserActivities { get; set; }
        ICollection<Post> Posts { get; set; }
        ICollection<Question> Questions { get; set; }
        ICollection<ShipInfo> ShippingAddresses { get; set; }
        ICollection<PickupInfo> PickupInfos { get; set; }
        ICollection<Cart> Carts { get; set; }
        ICollection<Order> Orders { get; set; }
        ICollection<Payment> Payments { get; set; }
        ICollection<ReviewUx> UXReviews { get; set; }
        ICollection<Review> Reviews { get; set; }
    }
}