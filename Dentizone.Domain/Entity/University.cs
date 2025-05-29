using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dentizone.Application.Interfaces;
using Dentizone.Domain;

namespace Dentizone.Infrastructure.Models
{
    internal class University : IBaseEntity
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
