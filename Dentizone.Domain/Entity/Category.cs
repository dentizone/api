using Dentizone.Domain.Interfaces;

namespace Dentizone.Domain.Entity
{
    public class Category : IBaseEntity, IUpdatable, IDeletable
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string Name { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public bool IsDeleted { get; set; }

        public virtual ICollection<Item> Items { get; set; } = new List<Item>();

        public virtual ICollection<SubCategory> SubCategories { get; set; } = new List<SubCategory>();
    }
}