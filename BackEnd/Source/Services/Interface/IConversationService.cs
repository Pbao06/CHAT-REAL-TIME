using Source.Dtos;
using Source.Models;
namespace Source.Services.Interface
{
    public interface IConversationService
    {
         Task<ConversationDto> GetOrCreateConversation(string userId, string receiveId);
    }
}
