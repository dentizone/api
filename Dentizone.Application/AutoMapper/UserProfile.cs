using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Dentizone.Application.DTOs.User;
using Dentizone.Domain.Entity;

namespace Dentizone.Application.AutoMapper
{
    public class UserProfile: Profile
    {
        public UserProfile()
        {
             CreateMap<UserDTO, AppUser>().ReverseMap();
        }
    }
}
