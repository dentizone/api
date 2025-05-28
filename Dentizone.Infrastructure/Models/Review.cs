using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dentizone.Domain;

namespace Dentizone.Infrastructure.Models
{
    internal class Review
    {

        public string Id { get; set; }
        public string UserId { get; set; }
        public string Stars { get; set; }
        public string? Text { get; set; }
        public string OrderId { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public AppUser User { get; set; }
        public Order Order { get; set; }

    } 
}

