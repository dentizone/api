using Dentizone.Domain.Interfaces;

namespace Dentizone.Domain.Entity
{
    public class University : IBaseEntity
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public bool IsSupported { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public string Domain { get; set; }

        public virtual ICollection<IAppUser> Users { get; set; } = new List<IAppUser>();
        public bool IsDeleted { get; set; }
    }
}