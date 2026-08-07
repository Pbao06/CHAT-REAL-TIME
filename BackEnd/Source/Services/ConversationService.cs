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
            if (!Guid.TryParse(userId, out var parsedUserId) || !Guid.TryParse(receiveId, out var parsedReceiveId))
            {
                throw new BadRequestException("Invalid user ID format.");
            }

            var user = await _context.Users.FindAsync(parsedUserId);
            if (user == null) throw new NotFoundException(" Not found user");
            var receiver = await _context.Users.FindAsync(parsedReceiveId);
            if (receiver == null) throw new NotFoundException(" Not found user Receive ");

            var check = await _context.Conversations.Include(c => c.MembersConversations)
            .Where(c => c.MembersConversations.Any(m => m.UserId == parsedUserId)
            && c.MembersConversations.Any(m => m.UserId == parsedReceiveId)
            && c.MembersConversations.Count == 2).FirstOrDefaultAsync();
            if (check.Id == null)
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
                    return new ConversationDto
                    {
                        Id = check.Id,
                        Name = check.Name,
                        AvatarUrl = check.AvatarUrl,
                        CreateAt = check.CreateAt,
                        UpdateAt = check.UpdateAt
                    };
                }
                catch (Exception)
                {
                    // neu error thi return lai -> dung ko thuc thi tiep tra kq ve ban dau 
                    await transaction.RollbackAsync();
                    throw; // nem ra error 
                }
            }
            var dtoo = new ConversationDto
            {
                Id = check.Id,
                Name = check.Name,
                AvatarUrl = check.AvatarUrl,
                CreateAt = check.CreateAt,
                UpdateAt = check.UpdateAt
            };
            return dtoo;
        }



    }
}