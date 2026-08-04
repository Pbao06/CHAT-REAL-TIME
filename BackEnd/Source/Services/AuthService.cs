using Source.Services.Interface;
using Source.Data;
using Source.Dtos;
using Source.Middleware;
using Microsoft.EntityFrameworkCore;
using BCrypt.Net;
namespace Source.Services
{
    public class AuthService : IAuthService
    {
        // sign ID
        private readonly ApplicationDbContext _context;
        public AuthService(ApplicationDbContext context) => _context = context;
        //public async Task Register()
        public async Task<RegisterRespone> Register(RegisterDto dto)
        {
            var exist = await _context.Users.FirstOrDefaultAsync(u => u.Email == dto.Email);   //(u=>u.Email==dto.Email)
            if (exist != null) throw new BadRequestException(" Account was exist cannot register");
            var mk= BCrypt.Net.BCrypt.HashPassword(dto.Password);
            var user = new Users
            {
                Username = dto.Username,
                Email = dto.Email,
                PasswordHash=mk
            };
            _context.Users.Add(user);
             await _context.SaveChangesAsync();
            return new RegisterRespone
            {
                UserName=dto.Username,
                Email=dto.Email,
            };
        }
    }
}