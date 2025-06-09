using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dentizone.Application.DTOs;

namespace Dentizone.Application.Interfaces
{
    public  interface IAuthentication
    {
        
        Task<bool> RegisterAsync(RegisterDTO dto);
        Task<string> LoginAsync(string academicEmail, string password);
        Task<bool> LogoutAsync();
        Task<bool> ResetPasswordAsync(string academicEmail, string newPassword);
    }
       
}
