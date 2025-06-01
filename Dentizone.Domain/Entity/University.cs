using Dentizone.Domain.Interfaces;

namespace Dentizone.Domain.Entity
{
    public class University : IBaseEntity, IDeletable, IUpdatable
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string Name { get; set; }
        public bool IsSupported { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public string Domain { get; set; }

        public virtual ICollection<AppUser> Users { get; set; } = new List<AppUser>();
        public bool IsDeleted { get; set; }
    }
}