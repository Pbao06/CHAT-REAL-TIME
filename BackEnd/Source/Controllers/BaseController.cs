using Microsoft.AspNetCore.Mvc;
using Source.Dtos;

namespace Source.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BaseController : ControllerBase
    {
        protected IActionResult Success<T>(T data, string message)
        {
            var kq = new ApiReponse<T>
            {
                Data = data,
                Message = message
            };
            return Ok(kq);
        }
    }
}