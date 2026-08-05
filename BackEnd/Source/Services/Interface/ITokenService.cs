
namespace Source.Services.Interface
{
    public  interface ITokenService
    {
         string GenerateToken(string username, string role);
    }

}