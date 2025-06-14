using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dentizone.Application.DTOs.PostFilterDTO
{
    public class CategoryFilterDTO
    {
        public string CategoryName { get; set; } 
        public List<string> Subcategories { get; set; } = new List<string>();
    }
}
