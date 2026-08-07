using Source.Services;
using Source.Data;
using Source.Models;
using Source.Services.Interface;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
namespace Source.Controllers
{
    public class ConversationController : BaseController
    {
        private readonly IConversationService _converService;
        public ConversationController(IConversationService converService) => _converService=converService;
        [HttpPost("GetOrCreate/{receiveId}")]
        public async Task<IActionResult> GetOrCreateConversation(string receiveId)
        {
            var userId= User.FindFirst(JwtRegisteredClaimNames.Sub)?.Value;
            var result= await _converService.GetOrCreateConversation(userId,receiveId);
            return Success(result," Success");
        }
    }
}