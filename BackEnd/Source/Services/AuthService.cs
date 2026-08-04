using Source.Services.Interface;
using Source.Data;
using Source.Dtos;
using Microsoft.EntityFrameworkCore;
namespace Source.Services
{
    public class AuthService : IAuthService
    {
        // sign ID
        private readonly ApplicationDbContext _context;
        public AuthService(ApplicationDbContext context)=> _context=context;
        //public async Task Register()
        public async Task<RegisterDto> Register(RegisterDto dto)
        {
           var exist= await _context.Users.FirstOrDefaultAsync(u=>u.Email==dto.Email);   //(u=>u.Email==dto.Email)
           if(exist==null) throw new Exception(" Khong tim thay ");
        }
    } 
}