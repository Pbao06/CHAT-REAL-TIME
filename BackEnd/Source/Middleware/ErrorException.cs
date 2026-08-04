using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using System.Net;
namespace Source.Middleware
{
    public class ErrorException
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ErrorException> _logger; // dung logger de bat loi
        // constructor 
        public ErrorException(RequestDelegate next, ILogger<ErrorException> logger)
        {
            _next = next;
            _logger = logger;
        }
        // ham invoke hung request va boc try catch 
        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex) // loi 500
            {
                //
                _logger.LogError(ex, " Da co loi xay ra {Message} ", ex.Message);
                // ben duoi catch nhung error chi tiet da dinh nghia ra 
            }
        }
        private static Task HandleExceptionAsync(HttpContext context, Exception exception, ILogger logger)
        {
            context.Response.ContentType = "application/json"; // goi du lieu dinh dang la json 
            // error mac dinh la 500 
            var statusCode = (int)HttpStatusCode.InternalServerError;
            var message = exception.Message;

            // kiem tra thong tin 
            // neu cac error la tu fatherException 
            if (exception is FatherError fatherError)
            {
                // laays truc tiep ma code 
                statusCode = (int)fatherError.StatusCode;
                message = fatherError.Message;
                logger.LogWarning(fatherError, " Custom Exception : {Message}", message);
            }
            else
            {
                // default  500 
                logger.LogWarning(exception, " Unhandle Exception : {Message}", exception.Message);
            }
            context.Response.StatusCode = statusCode;
            var response = new
            {
                status = statusCode,
                error = message
                // ngoai ra co the an detail de run tren production ( da deploy)
            };
            return context.Response.WriteAsJsonAsync(response);
        }
    }
}

