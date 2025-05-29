using Dentizone.Domain.Enums;
using Dentizone.Domain.Interfaces;

namespace Dentizone.Domain.Entity
{
    public class Post : IBaseEntity
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string SellerId { get; set; }
        public decimal Price { get; set; }
        public DateTime? ExpireDate { get; set; }
        public PostItemCondition Condition { get; set; }
        public PostStatus Status { get; set; }
        public string ItemId { get; set; }
        public string Slug { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public bool IsDeleted { get; set; }

        public virtual PickupInfo Pickupinfo { get; set; }

        public virtual AppUser Seller { get; set; }
        public virtual ICollection<Favourite> Favourites { get; set; }
        public virtual ICollection<PostAsset> PostAssets { get; set; } = new List<PostAsset>();
        public virtual ICollection<Question> Questions { get; set; } = new List<Question>();
        public virtual Item Item { get; set; }

        public virtual ICollection<Cart> Carts { get; set; } = new List<Cart>();
    }
}