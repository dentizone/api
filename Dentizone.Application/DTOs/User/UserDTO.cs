using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dentizone.Domain.Enums;
using FluentValidation;

namespace Dentizone.Application.DTOs.User
{
    public class UserDto
    {
        public string FullName { get; set; }
        public string Username { get; set; }
        public int AcademicYear { get; set; }
        public long? NationalId { get; set; }
        public KycStatus KycStatus { get; set; }
        public UserState Status { get; set; }
        public bool IsDeleted { get; set; }
        public string UniversityId { get; set; }
    }
}