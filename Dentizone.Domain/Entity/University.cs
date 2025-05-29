using Dentizone.Domain.Interfaces;
using AppUser = Dentizone.Domain.Entity.AppUser;

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

        public virtual ICollection<AppUser> Users { get; set; } = new List<AppUser>();
        public bool IsDeleted { get; set; }
    }
}