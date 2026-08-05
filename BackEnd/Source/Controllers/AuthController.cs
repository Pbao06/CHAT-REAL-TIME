using Microsoft.AspNetCore.Mvc;
using Source.Services.Interface;
using Source.Dtos;
namespace Source.Controllers
{
    public class AuthController : BaseController
    {
        private readonly IAuthService _authService;
        // contructor
        public AuthController(IAuthService authService)
        {
            _authService=authService;
        }
        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginDto dto)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result= await _authService.Login(dto);
            return Success(result," Login Success");
        }
        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto dto)
        {
            if(!ModelState.IsValid) return BadRequest(ModelState);
            var result= await _authService.Register(dto);
            return Success(result," Register Success");
        }
    }
}