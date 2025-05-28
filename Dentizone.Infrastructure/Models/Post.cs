
using Dentizone.Application.Interfaces;
using Dentizone.Domain;
using Dentizone.Domain.Enums;

namespace Dentizone.Infrastructure.Models
{
    internal class Post : IBaseEntity
    {
        public string Id { get; set; }
        public string title { get; set; }
        public string description { get; set; }
        public decimal price { get; set; }
        public DateTime? expire_date { get; set; }
        public PostItemCondition condition { get; set; }
        public PostStatus status { get; set; }
        public string item_id { get; set; }
        public string slug { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public bool IsDeleted { get; set; }

        public virtual PickupInfo pickupinfo {get; set;}

        public virtual AppUser Seller { get; set; }
       public string SellerId { get; set; }

        public ICollection<Question> questions { get; set; }


    }
}

