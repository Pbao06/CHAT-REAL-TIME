using Source.Data;
namespace Source.Services.Interface
{
    public  interface ITokenService
    {
         string GenerateToken(Users user);
    }

}