using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dentizone.Domain.Entity;
using FluentValidation;

namespace Dentizone.Application.DTOs.University
{
    public class UniversityView
    {
        public string Id { get; set; } 
        public string Name { get; set; }
        public bool IsSupported { get; set; }
        public string Domain { get; set; }
    }
}
