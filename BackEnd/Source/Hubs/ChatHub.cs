using Microsoft.AspNetCore.SignalR;
using Source.Data;
using Source.Models;
namespace Hubs
{
    public class ChatHubs : Hub
    {
        private readonly ApplicationDbContext _context;
        public ChatHubs(ApplicationDbContext context)=> _context=context;
        public async Task SendMessage(string user, string message)
        {
            // ham nay la la dong vai tro nh API Create tn luon 
           //... APi create 

            await Clients.All.SendAsync("ReceviceMessage", user, message);
            // Clients.All nghĩa là gửi đến toàn bộ các máy đang kết nối vào Hub
            // "ReceiveMessage" là tên sự kiện (event) mà bên Frontend (JS/React) sẽ lắng nghe
        }
    }
}