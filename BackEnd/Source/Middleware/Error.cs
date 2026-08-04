
using System.Net;
namespace Source.Middleware
{
    public class FatherError : Exception
    {
         // Middleware sẽ đọc thuộc tính này để biết trả về 400, 404 hay 500
        public HttpStatusCode StatusCode{get;set;}
        //Constructor này bắt buộc nhận StatusCode từ các lớp con truyền lên       
        protected FatherError(string message,HttpStatusCode statuscode) : base(message)
        {
            StatusCode=statuscode;
        }
    }

    public class BadRequestException : FatherError
    {
        // constructor 
        public BadRequestException(string message) : base(message,HttpStatusCode.BadRequest){} // 400
    }
    public class NotFoundException : FatherError
    {
        public NotFoundException(string message) : base(message,HttpStatusCode.NotFound){}//404
    }
    public class UnauthorizedException : FatherError // 401 chua dang nhap
    {
        public UnauthorizedException(string message) : base(message,HttpStatusCode.Unauthorized){}
    }
    public class ForBiddenException : FatherError // 403 khong co quyen truy cap 
    {
        public  ForBiddenException(string message) : base(message,HttpStatusCode.Forbidden){}
    }
    public class ConflictException : FatherError // 409 
    {
        //constructor 
         public ConflictException(string message) : base(message,HttpStatusCode.Conflict){}
    }

}