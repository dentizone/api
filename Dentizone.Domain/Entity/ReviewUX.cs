using Dentizone.Domain.Enums;
using Dentizone.Domain.Interfaces;

namespace Dentizone.Domain.Entity
{
    public class ReviewUx : IBaseEntity
    {
        public string Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public bool IsDeleted { get; set; }


        public string UserId { get; set; }
        public virtual IAppUser User { get; set; }

        public virtual Order Order { get; set; }
        public string OrderId { get; set; }

        public string? Text { get; set; }
        public int Stars { get; set; }

        public ReviewStatus Status { get; set; }
    }
}