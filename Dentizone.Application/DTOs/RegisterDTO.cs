using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dentizone.Domain.Enums;

namespace Dentizone.Application.DTOs
{
    public class RegisterDTO
    {
        public string FullName { get; set; }
      
        public string AcademicEmail { get; set; }
        public string Password { get; set; }
        public string UniversityId { get; set; }
        public int AcademicYear { get; set; }



    }
}
