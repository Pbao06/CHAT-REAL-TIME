namespace Source.Dtos
{
    public class MemberConversationDto
    {
        public string UserId{get;set;}
        public int ConversationId{get;set;}
        public DateTime CreateAt{get;set;}=DateTime.Now;// ngày tạo cuộc trò truyện
        public DateTime UpdateAt{get;set;}=DateTime.Now;// ngày cập nhật cuộc trò truyện
        public bool IsAdmin{get;set;}=false;// quyền admin của người dùng trong cuộc trò truyện
    }
}