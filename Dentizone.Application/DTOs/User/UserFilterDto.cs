using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dentizone.Domain.Enums;

namespace Dentizone.Application.DTOs.User
{
    public class UserFilterDto
    {
        public string? SearchTerm { get; set; }
        public UserState? Status { get; set; }
        public int Page { get; set; } = 1;
    }
}