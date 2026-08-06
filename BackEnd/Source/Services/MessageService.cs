using Source.Services.Interface;
using Source.Dtos;
using Source.Data;
using Source.Models;
using Source.Middleware;
using Microsoft.EntityFrameworkCore;
namespace Source.Services
{
    public class MessageService: IMessageService
    {
        private readonly ApplicationDbContext _context;
        public MessageService(ApplicationDbContext context)
        {
            _context=context;
        }
        public async Task<List<MessageDto>> GetAllMessage(string userId,int ConversationId)
        {
            var user= await _context.Users.FindAsync(userId);
            if(user==null) throw new NotFoundException(" Not Found User");
            var conver= await _context.Conversations.FindAsync(ConversationId);
            if(conver==null) throw new NotFoundException(" Not found Conversation for load message history");
            var result= await _context.Messages.Include(m=>m.Sender).Where(m=> m.ConversationId==conver.Id).ToListAsync();
            var dtoo= result.Select(x=> new MessageDto
            {
                UserName=x.Sender.Username,
                Content= x.Content,
                SentAt=x.SentAt
            }).ToList();
            return dtoo;
        }
    }
}