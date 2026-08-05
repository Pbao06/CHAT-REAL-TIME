using Source.Dtos;
namespace Source.Services.Interface
{
    public interface IAuthService 
    {
        Task<RegisterRespone> Register(RegisterDto dto);
        Task<AuthResponse> Login(LoginDto dto);
    }
}