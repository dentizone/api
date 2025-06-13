using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dentizone.Domain.Enums;

namespace Dentizone.Application.DTOs.PostDTO
{
    public class UpdatePostDto
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public PostItemCondition Condition { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public DateTime? ExpireDate { get; set; }
    }
}