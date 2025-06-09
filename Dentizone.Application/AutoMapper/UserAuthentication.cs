using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Dentizone.Application.DTOs;
using Dentizone.Domain.Entity;
using Dentizone.Infrastructure.Identity;

namespace Dentizone.Application.AutoMapper
{
    internal class UserAuthentication: Profile
    {
        public UserAuthentication()
        {
            CreateMap<RegisterDTO,ApplicationUser>().ReverseMap();
            CreateMap<LoginDTO, ApplicationUser>().ReverseMap();



        }
    }
   
}
