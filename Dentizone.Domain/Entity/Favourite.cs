using Dentizone.Domain.Interfaces;

namespace Dentizone.Domain.Entity
{
    public class Favourite : IBaseEntity
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string UserId { get; set; }
        public string PostId { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public bool IsDeleted { get; set; }
        public virtual AppUser User { get; set; }
        public virtual Post Post { get; set; }
    }
}