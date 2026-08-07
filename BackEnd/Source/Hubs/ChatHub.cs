using Microsoft.AspNetCore.SignalR;
using Source.Data;
using Source.Models;
namespace Hubs
{
    public class ChatHubs : Hub
    {
        private readonly ApplicationDbContext _context;
        public ChatHubs(ApplicationDbContext context) => _context = context;
        public async Task JoinConversation(string conversationId) // khi user join conversation -> thiet lap realtime 
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, conversationId);
        }
        public async Task LeaveConversation(string conversationId)// khi user thoat phong chat huy ket noi real time 
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, conversationId);
        }

    }
}