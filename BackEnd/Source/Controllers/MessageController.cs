using Source.Services.Interface;
using Source.Dtos;
using Microsoft.AspNetCore.Mvc;
using Source.Data;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Authorization;

namespace Source.Controllers
{
    [Authorize]
    public class MessageController : BaseController
    {
        private readonly IMessageService _messageService;
        public MessageController(IMessageService messageService)
        {
            _messageService = messageService;
        }

        public async Task<IActionResult> GetMessage(int conversationId)
        {
            // lay id user tu token 
            var userId = User.FindFirst(JwtRegisteredClaimNames.Sub)?.Value;
            var result = await _messageService.GetAllMessage(userId,conversationId);
            return Success(result," Get Success ");
        }
    }
}