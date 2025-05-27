using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dentizone.Application.Interfaces;

namespace Dentizone.Infrastructure.Models
{
    internal class SubCategory: IBaseEntity
    {
        public string Id { get; set; }

        public string Name { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public bool IsDeleted { get; set; }

        
        // Relationship: Many SubCategories to One Category
        public virtual Category Category { get; set; }
        public string CategoryId { get; set; } // Foreign Key for Category

        // Relationship: One SubCategory to Many Items
        //public virtual ICollection<Item> Items { get; set; } = new List<Item>();


    }

}
