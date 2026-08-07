using Source.Services.Interface;
using Source.Dtos;
using Microsoft.AspNetCore.Mvc;
using Source.Data;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Authorization;
using Hubs;
using Microsoft.AspNetCore.SignalR;
namespace Source.Controllers
{
    [Authorize]
    public class MessageController : BaseController
    {
        private readonly IMessageService _messageService;
        private readonly IHubContext<ChatHubs> _chathub;
        public MessageController(IMessageService messageService, IHubContext<ChatHubs> chathub)
        {
            _messageService = messageService;
            _chathub = chathub;
        }
        [HttpPost("Send/{conversationId}")]
        public async Task<IActionResult> SendMessage(int conversationId, [FromBody] MessageDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var userId = User.FindFirst(JwtRegisteredClaimNames.Sub)?.Value;
            var result = await _messageService.SendMessage(userId, conversationId, dto);
            //Dùng SignalR bắn tin nhắn xuống các client đang trong phòng
            await _chathub.Clients.Group(conversationId.ToString()).SendAsync("ReceiveMessage", result);
            // receiveMessage la event ma frontend se nghe 
            return Success(result, " Send message success");
        }
        [HttpGet("GetMessage")]
        public async Task<IActionResult> GetMessage(int conversationId)
        {
            // lay id user tu token 
            var userId = User.FindFirst(JwtRegisteredClaimNames.Sub)?.Value;
            var result = await _messageService.GetAllMessage(userId, conversationId);
            return Success(result, " Get Success ");
        }
    }
}