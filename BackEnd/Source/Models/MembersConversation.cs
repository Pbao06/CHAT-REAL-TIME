using Source.Data;
using Source.Models;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
namespace Source.Models
{
    public class MembersConversation
    {
        public int Id{get;set;}
    
        public Guid UserId{get;set;}
        [ForeignKey("UserId")]
        public Users? User{get;set;} // N-1
        public int ConversationId{get;set;}
        [ForeignKey("ConversationId")]
        public Conversation? Conversation{get;set;}// N-1
        public DateTime CreateAt{get;set;}=DateTime.Now;// ngày tạo cuộc trò truyện
        public DateTime UpdateAt{get;set;}=DateTime.Now;// ngày cập nhật cuộc trò truyện
        public bool IsAdmin{get;set;}=false;// quyền admin của người dùng trong cuộc trò truyện
        // navigation properties
        public ICollection<Users>? Users{get;set;} // 1-N
        public ICollection<Conversation>? Conversations{get;set;} // 1-N
    }
}