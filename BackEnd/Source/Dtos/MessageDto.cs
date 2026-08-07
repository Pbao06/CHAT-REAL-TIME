using System.ComponentModel.DataAnnotations;

namespace  Source.Dtos
{
    public class ListMessageDto
    {
        [Required(ErrorMessage=" User Name cannot null")]
        public string UserName{get;set;}
        public string Content{get;set;}
        public DateTime SentAt{get;set;}
    }
    public class MessageDto
    {
        [Required(ErrorMessage=" Message not accept null")]
        public string Content{set;get;}
    }
    public class MessageRespondDto
    {   
        public int Id { get; set; }
        public int ConversationId { get; set; }
        public Guid SenderId { get; set; } 
        public string Content { get; set; } = string.Empty; // content messsage 
        public DateTime SentAt { get; set; } = DateTime.Now;// message at 
        
    }
}