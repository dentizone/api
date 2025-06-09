using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Dentizone.Application.Services
{
    public class TokenGeneration
    {
        private readonly IConfiguration _configuration;
        public TokenGeneration(IConfiguration configuration)

        {
            _configuration = configuration;

        }

        public string GenerateToken(string username, string email, string password)
        {
            // Build claims here
            var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, username),
            new Claim(ClaimTypes.Email, email)
            
        };
            //generate key 
            var secretKey = _configuration.GetValue<string>("Jwt:SecretKey");
            var keyBytes = Encoding.ASCII.GetBytes(secretKey);
            var key = new SymmetricSecurityKey(keyBytes);
            //determine how to generate hashing result:3rd part y3ni el feh el security key
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature);
            //current token
            var Jwt = new JwtSecurityToken(
                     claims: claims,
                     notBefore: DateTime.Now,
                     expires: DateTime.Now.AddMinutes(15),
                     signingCredentials: credentials);
            var tokenhandler = new JwtSecurityTokenHandler();
            string tokenstring = tokenhandler.WriteToken(Jwt);
            return tokenstring;
        }

       
    }
}

