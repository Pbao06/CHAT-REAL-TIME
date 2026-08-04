using System.ComponentModel.DataAnnotations.Schema;
using Source.Data;
using Source.Models;
namespace Source.Models
{
    public class Message
    {
        public int Id { get; set; }
        public int ConversationId { get; set; }
        [ForeignKey("ConversationId")]
        public Conversation? Conversation{get;set;} // N-1
        public Guid SenderId { get; set; } 
        [ForeignKey("SenderId")]
        public Users? Sender { get; set; } // N-1
        public string Content { get; set; } = string.Empty; // content messsage 
        public DateTime SentAt { get; set; } = DateTime.Now;// message at 
    }
}