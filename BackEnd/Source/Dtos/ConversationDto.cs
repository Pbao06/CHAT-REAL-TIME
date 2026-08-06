
using System.ComponentModel.DataAnnotations;

namespace Source.Dtos
{
    public class ConversationDto
    {
        [Required(ErrorMessage=" Id not null ")]
        public int Id{get;set;} // nó phải là tự động sinh id+1
        public string? Name{get;set;}// tên cuộc trò truyện 
        public string? AvatarUrl{get;set;}// ảnh đại diện của cuộc trò truyện
        public DateTime CreateAt{get;set;}=DateTime.Now; // ngày tạo cuộc trò truyện
        public DateTime UpdateAt{get;set;}=DateTime.Now; // ngày cập nhật cuộc trò truyện
    }
}