using Dentizone.Domain.Interfaces;

namespace Dentizone.Domain.Entity
{
    public class SubCategory : IBaseEntity, IDeletable, IUpdatable
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();

        public string Name { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public bool IsDeleted { get; set; }


        // Relationship: Many SubCategories to One Category
        public virtual Category Category { get; set; }
        public string CategoryId { get; set; } // Foreign Key for Category
    }
}