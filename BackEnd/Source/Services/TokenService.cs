using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System;
using Source.Services.Interface;
using Source.Dtos;
using Source.Models;
using Microsoft.Extensions.Options;
using System.Text;
using Source.Data;
namespace Source.Services
{
    public class TokenService : ITokenService
    {
        private readonly JwtSettings _jwtSettings;
        public TokenService(IOptions<JwtSettings> jwtSettings)
        {
            _jwtSettings = jwtSettings.Value;
        }
        public string GenerateToken(Users user)
        {
            // get config secretkey 
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.SecretKey));
            // ready info to sign 
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub,user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Sub, user.Username),
                new Claim(ClaimTypes.Role,user.Role.ToString()),
            };
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddHours(_jwtSettings.ExpireHours),
                Issuer = _jwtSettings.Issuer,
                Audience = _jwtSettings.Audience,
                SigningCredentials = credentials
            };
            var tokenHandler = new JwtSecurityTokenHandler(); // goi thang ham chuyen xu ly jwt 
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}