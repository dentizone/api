using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dentizone.Application.Interfaces;

namespace Dentizone.Infrastructure.Models
{
    internal class Category : IBaseEntity
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public bool IsDeleted { get; set; }

        //public virtual ICollection<Item> Items { get; set; } = new List<Item>();

        public virtual ICollection<SubCategory> SubCategories { get; set; } = new List<SubCategory>();

    }
}
