using Source.Services.Interface;
using Source.Data;
using Microsoft.EntityFrameworkCore; // Required namespace
using Source.Dtos;
using Source.Middleware;
using System.Linq;
using Source.Models;
namespace Source.Services
{
    public class ConversationService : IConversationService
    {
        private readonly ApplicationDbContext _context;
        public ConversationService(ApplicationDbContext context) => _context = context;

        // User -> press chat -> create Conversation if not exist before 
        // if exist -> get -> to show it 
        // situation -> chat 1 vs 1 not group 
        public async Task<ConversationDto> GetOrCreateConversation(string userId, string receiveId)
        {
            var user = await _context.Users.FindAsync(userId);
            if (user == null) throw new NotFoundException(" Not found user");
            var receiver = await _context.Users.FindAsync(receiveId);
            if (receiver == null) throw new NotFoundException(" Not found user Receive ");
            var converA = await _context.MembersConversations.
            Where(m => m.UserId == user.Id).Select(m => m.ConversationId).ToListAsync();
            var ConversationB = await _context.MembersConversations.Where(m => m.UserId == Guid.Parse(receiveId)).Select(m => m.ConversationId).ToListAsync();
            var check = converA.Intersect(ConversationB).ToList();
            if(check.Count > 1) throw new BadRequestException(" Conversation not legal");
            if (check.Count == 0)
            {
                using var transaction = await _context.Database.BeginTransactionAsync();
                try
                {
                    // neu khong co ban ghi nao thi create Conversation
                    var conversation = new Conversation
                    {
                        Name = string.Empty,
                        AvatarUrl = string.Empty,

                    };
                    _context.Conversations.Add(conversation);
                    await _context.SaveChangesAsync();
                    // sau khi da co id roi -> dung de insert cho member vao thoi 
                    var userA = new MembersConversation
                    {
                        UserId = user.Id,
                        ConversationId = conversation.Id,
                    };
                    _context.MembersConversations.Add(userA);
                    var userB = new MembersConversation
                    {
                        UserId = receiver.Id,
                        ConversationId = conversation.Id
                    };
                    _context.MembersConversations.Add(userB);
                    await _context.SaveChangesAsync();
                    await transaction.CommitAsync(); // save 
                    var siu = new ConversationDto
                    {
                        Id = conversation.Id,
                        Name = conversation.Name,
                        AvatarUrl = conversation.AvatarUrl,
                        CreateAt = conversation.CreateAt,
                        UpdateAt = conversation.UpdateAt
                    };
                    return siu;
                }
                catch (Exception)
                {
                    // neu error thi return lai -> dung ko thuc thi tiep tra kq ve ban dau 
                    await transaction.RollbackAsync();
                    throw; // nem ra error 
                }
            }
            
                var conversationId = check.FirstOrDefault();
                var query = await _context.Conversations.FirstOrDefaultAsync(c => c.Id == conversationId);
                if (query==null) throw new BadRequestException(" Cannot get Id conversation");
                var dtoo = new ConversationDto
                {
                    Id = query.Id,
                    Name = query.Name,
                    AvatarUrl = query.AvatarUrl,
                    CreateAt = query.CreateAt,
                    UpdateAt = query.UpdateAt
                };
                return dtoo;
        }



    }
}