using Microsoft.EntityFrameworkCore;
using Source.Models;
using System.ComponentModel.DataAnnotations.Schema;
namespace Source.Data
{
    public class Conversation
    {
        public int Id{get;set;} // nó phải là tự động sinh id+1
        public string? Name{get;set;}// tên cuộc trò truyện 
        public string? AvatarUrl{get;set;}// ảnh đại diện của cuộc trò truyện
        public DateTime CreateAt{get;set;}=DateTime.Now; // ngày tạo cuộc trò truyện
        public DateTime UpdateAt{get;set;}=DateTime.Now; // ngày cập nhật cuộc trò truyện
        public ICollection<MembersConversation>? MembersConversations{get;set;} // danh sách các thành viên trong cuộc trò truyện
        public ICollection<Message>? Messages{get;set;} // danh sách các tin nhắn trong cuộc trò truyện
    }
}