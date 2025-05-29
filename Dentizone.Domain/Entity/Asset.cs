using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dentizone.Application.Interfaces;
using Dentizone.Domain.Enums;


namespace Dentizone.Infrastructure.Models
{
    internal class Asset : IBaseEntity
    {
        public string Id { get; set; }
        public string Url { get; set; }
        public long Size { get; set; }
        public AssetType Type { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public AssetStatus Status { get; set; }
        public bool IsDeleted { get; set; }
        public virtual ICollection<UserAsset> UserAssets { get; set; } = new List<UserAsset>();
        public virtual ICollection<PostAsset> PostAssets { get; set; } = new List<PostAsset>();
    }

}
