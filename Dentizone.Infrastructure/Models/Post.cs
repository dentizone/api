using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dentizone.Infrastructure.Models;
using Dentizone.Application.Interfaces;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Dentizone.Infrastructure.Models
{
    internal class Post : IBaseEntity
    {
        public string Id { get; set; }
        public decimal title { get; set; }
        public string description { get; set; }
        public decimal price { get; set; }
        public DateTime expire_date { get; set; }
        public string condition { get; set; }
        public string status { get; set; }
        public string seller_id { get; set; }
        public string item_id { get; set; }
        public string slug { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public bool IsDeleted { get; set; }

       // public PickupInfo pickupinfo {get; set;}
       public User user { get; set; }


    }
}

