using Dentizone.Application.Interfaces;

namespace Dentizone.Infrastructure.Models
{
    internal class Item : IBaseEntity
    {

        public string Id { get; set; }
        public string CategoryId { set; get; }
        public string SubCategoryId { set; get; }


        public virtual Category Category { get; set; }
        public virtual SubCategory SubCategory { get; set; }

        public virtual Post Post { get; set; }
        public DateTime CreatedAt { get; set ; }
        public DateTime UpdatedAt { get; set ; }
        public bool IsDeleted { get; set; }
    }
}
