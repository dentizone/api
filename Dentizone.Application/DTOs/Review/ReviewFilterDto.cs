using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dentizone.Application.DTOs.Review
{
    public class ReviewFilterDto
    {
        public int? Stars { get; set; }
        public string? Sentiment { get; set; }
        public DateTime? CreatedAfter { get; set; }
        public DateTime? CreatedBefore { get; set; }
        public string? SearchText { get; set; }

        public int Page { get; set; } = 1;
    }
}