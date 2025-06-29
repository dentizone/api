using Dentizone.Domain.Interfaces;

namespace Dentizone.Domain.Entity
{
    public class Cart : IBaseEntity, IDeletable
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public required string UserId { get; set; }
        public required string PostId { get; set; }
        public DateTime CreatedAt { get; set; }

        public bool IsDeleted { get; set; }

        // Navigation properties
        public virtual AppUser User { get; set; } = new();
        public virtual Post Post { get; set; }
    }
}