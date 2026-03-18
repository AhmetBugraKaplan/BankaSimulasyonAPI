using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using BankaSimulasyon.Models.Entities;
using Microsoft.IdentityModel.Tokens;

namespace BankaSimulasyon.Services
{
    public class JwtService
    {
        private readonly IConfiguration _configuration;

        public JwtService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string TokenUret(string kartNumara, int atmId)
        {
            //Claimler tokenlerin içine gömülü olan bilgilerdir ve bu kısımda oluşturuyoruz.
            //bilgileri bizim kullanici entitymizden alıyor.
            var claims = new[]
            {
                new Claim("kartNumara",kartNumara),
                new Claim("atmId",atmId.ToString())
            };

            var key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]!)
            );

            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(
                    Convert.ToDouble(_configuration["Jwt:ExpireMinutes"])
                ),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

    }
}