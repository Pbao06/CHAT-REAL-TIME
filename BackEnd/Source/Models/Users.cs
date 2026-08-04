using System;
using Source.Models;
namespace Source.Data
{
    public class Users
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Username { get; set; }=string.Empty;
        public string Email { get; set; }=string.Empty;
        public string PasswordHash { get; set; }=string.Empty;
        public Role Role { get; set; }=Role.User; // default is user 
        public string? AvartarUrl{get;set;}=string.Empty;
        public ICollection<MembersConversation>? MembersConversations{get;set;} // danh sách các cuộc trò truyện mà người
        public ICollection<Message>? Messages{get;set;} // danh sách các tin nhắn mà người dùng đã gửi
    }
}