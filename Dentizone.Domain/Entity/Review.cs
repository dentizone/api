using Dentizone.Domain.Interfaces;

namespace Dentizone.Domain.Entity
{
    public class Review : IBaseEntity, IDeletable, IUpdatable
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string UserId { get; set; } = string.Empty;
        public int Stars { get; set; }
        public string? Text { get; set; }
        public string OrderId { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public virtual AppUser User { get; set; }
        public virtual Order Order { get; set; }
        public bool IsDeleted { get; set; }
    }
}