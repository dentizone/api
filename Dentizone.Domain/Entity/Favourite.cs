using Dentizone.Domain.Interfaces;

namespace Dentizone.Domain.Entity
{
    public class Favourite : IBaseEntity, IDeletable
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public required string UserId { get; set; }
        public required string PostId { get; set; }
        public DateTime CreatedAt { get; set; }
        public bool IsDeleted { get; set; }
        public virtual AppUser User { get; set; }
        public virtual Post Post { get; set; }
    }
}