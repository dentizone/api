using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dentizone.Application.Interfaces;

namespace Dentizone.Infrastructure.Models
{
    internal class PostAsset: IBaseEntity
    {
        public string Id { get; set; }
        public string PostId { get; set; }
        public string AssetId { get; set; }
        public int DisplayOrder  { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public bool IsDeleted { get; set; }
        // Navigation properties
        public virtual Post Post { get; set; }
        public virtual Asset Asset { get; set; }
    }
}
