using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dentizone.Application.DTOs.PostFilterDTO
{
    public class SidebarFilterDto
    {
        public List<string> Cities { get; set; } = new List<string>();
        public List<CategoryFilterDto> Categories { get; set; } = new List<CategoryFilterDto>();
        public decimal MinPrice { get; set; }
        public decimal MaxPrice { get; set; }
    }
}