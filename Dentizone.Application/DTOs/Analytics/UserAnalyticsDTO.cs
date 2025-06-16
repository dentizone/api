using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;

namespace Dentizone.Application.DTOs.Analytics
{
   public class UserAnalyticsDTO
    {
        public int TotalUsers { get; set; }
        public int NewUsersLast7Days{ get; set; }
        public int NewUsersLast30Days { get; set; }
      
        public Dictionary<string, int> UsersByUniversity { get; set; } = new Dictionary<string, int>();
    }
   // 7sa msh logic ani a3ml validation 
    
}
