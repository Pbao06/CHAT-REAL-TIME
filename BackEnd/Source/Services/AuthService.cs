using Source.Services.Interface;
using Source.Data;
using Source.Dtos;
using Source.Middleware;
using Microsoft.EntityFrameworkCore;
using BCrypt.Net;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
namespace Source.Services
{
    public class AuthService : IAuthService
    {
        // sign ID
        private readonly ApplicationDbContext _context;
        private readonly ITokenService _tokenService;
        public AuthService(ApplicationDbContext context,ITokenService tokenService)
        {
            _context=context;
            _tokenService=tokenService;
        }
        //public async Task Register()
        public async Task<RegisterRespone> Register(RegisterDto dto)
        {
            var exist = await _context.Users.FirstOrDefaultAsync(u => u.Email == dto.Email);   //(u=>u.Email==dto.Email)
            if (exist != null) throw new BadRequestException(" Account was exist cannot register");
            var mk = BCrypt.Net.BCrypt.HashPassword(dto.Password);
            var user = new Users
            {
                Username = dto.Username,
                Email = dto.Email,
                PasswordHash = mk
            };
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return new RegisterRespone
            {
                UserName = dto.Username,
                Email = dto.Email,
            };
        }
        public async Task<AuthResponse> Login(LoginDto dto)
        {
            var exist = await _context.Users.FirstOrDefaultAsync(u => u.Email == dto.Email);
            if (exist == null) throw new NotFoundException(" Not Found User Email");

            // neu nhu email da ton tai == roi -> chuyen qua so sanh mk 
            bool isValid = BCrypt.Net.BCrypt.Verify(dto.Password, exist.PasswordHash);
            if (!isValid) throw new UnauthorizedException(" Email or Password not valid");
            // th mat khau khop -> login thanh cong -> tao token 
            var token= _tokenService.GenerateToken(exist.Username,exist.Role.ToString());
            var response= new AuthResponse
            {
                Email=exist.Email,
                UserName=exist.Username,
                Token=token
            };
            return response;
        }

       
    }
}