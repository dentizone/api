using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dentizone.Application.DTOs.Analytics
{
    public class PostAnalyticsDTO
    {
        public int TotalPosts { get; set; }

        public int AveragePostPrice { get; set; }
        public Dictionary<string, int> PostsByCategory { get; set; } = new Dictionary<string, int>();

    }
}
