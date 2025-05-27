using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dentizone.Infrastructure.Models
{
    internal class University
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public bool IsSupported { get; set; }
        public DateTime AddedAt { get; set; }
        public string Domain { get; set; }
    }
}
