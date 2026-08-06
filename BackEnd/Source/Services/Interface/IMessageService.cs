using Source.Models;
using Source.Dtos;
namespace Source.Services.Interface
{
    public interface IMessageService
    {
         Task<List<MessageDto>> GetAllMessage(string userId,int ConversationId);
    }
}