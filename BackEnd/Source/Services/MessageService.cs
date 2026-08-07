using Source.Services.Interface;
using Source.Dtos;
using Source.Data;
using Source.Models;
using Source.Middleware;
using Microsoft.EntityFrameworkCore;
namespace Source.Services
{
    public class MessageService : IMessageService
    {
        private readonly ApplicationDbContext _context;
        public MessageService(ApplicationDbContext context)
        {
            _context = context;
        }
        // Create Message 
        public async Task<MessageRespondDto> SendMessage(string userId, int conversationId, MessageDto dto)
        {
            if (!Guid.TryParse(userId, out var parsedUserId))
            {
                throw new BadRequestException(" Wrong data type guid - string");
            }
            if (string.IsNullOrWhiteSpace(dto.Content))throw new BadRequestException("Message cannot null or space white ");
            var existConver = await _context.Conversations.Where(c => c.MembersConversations.Any(m => m.UserId == parsedUserId) && c.Id == conversationId).FirstOrDefaultAsync();
            if (existConver == null) throw new NotFoundException(" Not Exist Conversation !");
            existConver.UpdateAt=DateTime.UtcNow;
            var newMessage = new Message
            {
                ConversationId = existConver.Id,
                SenderId = parsedUserId,
                Content = dto.Content,
                // nhung cai con lai khi create no se tu tao 
            };
            _context.Messages.Add(newMessage);
            await _context.SaveChangesAsync();
            var dtoo = new MessageRespondDto
            {
                Id=newMessage.Id,
                SenderId=newMessage.SenderId,
                Content = newMessage.Content,
                SentAt = newMessage.SentAt
            };
            return dtoo;
        }
        public async Task<List<ListMessageDto>> GetAllMessage(string userId, int ConversationId)
        {
            var user = await _context.Users.FindAsync(userId);
            if (user == null) throw new NotFoundException(" Not Found User");
            var conver = await _context.Conversations.FindAsync(ConversationId);
            if (conver == null) throw new NotFoundException(" Not found Conversation for load message history");
            var result = await _context.Messages.Include(m => m.Sender).Where(m => m.ConversationId == conver.Id).ToListAsync();
            var dtoo = result.Select(x => new ListMessageDto
            {
                UserName = x.Sender.Username,
                Content = x.Content,
                SentAt = x.SentAt
            }).ToList();
            return dtoo;
        }
    }
}