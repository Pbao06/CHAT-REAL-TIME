using Source.Models;
using Source.Dtos;
namespace Source.Services.Interface
{
    public interface IMessageService
    {
         Task<List<ListMessageDto>> GetAllMessage(string userId,int ConversationId);
         Task<MessageRespondDto> SendMessage(string userId, int conversationId, MessageDto dto);
    }
}