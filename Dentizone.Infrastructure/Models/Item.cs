using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dentizone.Application.Interfaces;

namespace Dentizone.Infrastructure.Models
{
    internal class Item : IBaseEntity
    {

        public string Id { get; set; }
        public string category_id { set; get; }
        public string sub_category_id { set; get; }


        public virtual Category Category { get; set; }
        public virtual SubCategory SubCategory { get; set; }
        public DateTime CreatedAt { get; set ; }
        public DateTime UpdatedAt { get; set ; }
        public bool IsDeleted { get; set; }
        public ICollection<Post> Posts { get; set; } = new List<Post>();
        public Post Post { get; set; }
    }
}
