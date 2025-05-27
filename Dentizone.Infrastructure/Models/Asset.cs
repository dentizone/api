using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dentizone.Domain.Enums;


namespace Dentizone.Infrastructure.Models
{
    internal class Asset
    {
        public string Id { get; set; }
        public string Url { get; set; }
        public long Size { get; set; }
        public AssetType Type { get; set; }
        public DateTime CreatedAt { get; set; }
        public AssetStatus Status { get; set; }  
        public bool IsDeleted { get; set; }
    }
}
