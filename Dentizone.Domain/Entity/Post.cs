using Dentizone.Domain.Enums;
using Dentizone.Domain.Interfaces;

namespace Dentizone.Domain.Entity
{
    public class Post : IBaseEntity, IUpdatable, IDeletable
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string SellerId { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public DateTime? ExpireDate { get; set; }
        public PostItemCondition Condition { get; set; }
        public PostStatus Status { get; set; } = PostStatus.Active;
        public string Slug { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public bool IsDeleted { get; set; }
        public string Street { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public virtual AppUser Seller { get; set; }
        public virtual ICollection<Favourite> Favourites { get; set; }
        public virtual ICollection<PostAsset> PostAssets { get; set; } = new List<PostAsset>();
        public virtual ICollection<Question> Questions { get; set; } = new List<Question>();
        public string CategoryId { set; get; } = string.Empty;
        public string SubCategoryId { set; get; } = string.Empty;
        public virtual Category Category { get; set; } = new();
        public virtual SubCategory SubCategory { get; set; } = new();
        public virtual ICollection<Cart> Carts { get; set; } = [];
    }
}