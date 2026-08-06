using System.ComponentModel.DataAnnotations;

namespace  Source.Dtos
{
    public class MessageDto
    {
        [Required(ErrorMessage=" User Name cannot null")]
        public string UserName{get;set;}
        public string Content{get;set;}
        public DateTime SentAt{get;set;}
    }
}