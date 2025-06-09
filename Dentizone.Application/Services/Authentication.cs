using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Dentizone.Application.DTOs;
using Dentizone.Application.Interfaces;
using Dentizone.Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace Dentizone.Application.Services
{
    internal class Authentication: IAuthentication
    {
        private readonly IMapper _mapper;
        private readonly IAuthentication _userAuthentication;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly TokenGeneration _tokenGenerator;
        public Authentication(IMapper map,IAuthentication userAuthentication, UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, TokenGeneration tokenGenerator)
        {
            _mapper = map;
            _userAuthentication = userAuthentication;
            _userManager = userManager;
            _signInManager = signInManager;
            _tokenGenerator = tokenGenerator;


        }
        public async Task<bool> RegisterAsync(RegisterDTO dto)
        { 
            var user = _mapper.Map<ApplicationUser>(dto);
            var result = await _userManager.CreateAsync(user, dto.Password);
            return result.Succeeded;

        }

        public async Task<string> LoginAsync(string academicEmail, string password)
        {
            var user = await _userManager.FindByEmailAsync(academicEmail);
            if (user == null)
            {
                throw new Exception("User not found.");
            }
            var result = await _signInManager.CheckPasswordSignInAsync(user, password, false);
            var NewToken = _tokenGenerator.GenerateToken(user.UserName, user.Email, password);

            return NewToken;
        }

        public  async Task<bool> LogoutAsync()
        {
            await _signInManager.SignOutAsync();
            return true;
        }

       

        public  async Task<bool> ResetPasswordAsync(string academicEmail, string newPassword)
        {
            var user = await _userManager.FindByEmailAsync(academicEmail);
            if (user == null)
                throw new Exception("User not found.");

            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            var result = await _userManager.ResetPasswordAsync(user, token, newPassword);
            return result.Succeeded;
        }
        




    }
}
