using Dentizone.Domain.Enums;
using Dentizone.Domain.Interfaces;

namespace Dentizone.Domain.Entity
{
    public class Post : IBaseEntity, IUpdatable, IDeletable
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string Title { get; set; }
        public string Description { get; set; }
        public string SellerId { get; set; }
        public decimal Price { get; set; }
        public DateTime? ExpireDate { get; set; }
        public PostItemCondition Condition { get; set; }
        public PostStatus Status { get; set; } = PostStatus.Active;
        public string Slug { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public bool IsDeleted { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public virtual AppUser Seller { get; set; }
        public virtual ICollection<Favourite> Favourites { get; set; }
        public virtual ICollection<PostAsset> PostAssets { get; set; } = new List<PostAsset>();
        public virtual ICollection<Question> Questions { get; set; } = new List<Question>();
        public string CategoryId { set; get; }
        public string SubCategoryId { set; get; }
        public virtual Category Category { get; set; }
        public virtual SubCategory SubCategory { get; set; }
        public virtual ICollection<Cart> Carts { get; set; } = new List<Cart>();
    }
}