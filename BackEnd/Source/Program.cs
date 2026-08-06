
using Microsoft.EntityFrameworkCore;
using Source.Data;
using Source.Services;
using Source.Services.Interface;
using Source.Middleware;
using Microsoft.AspNetCore.Builder;
using Source.Dtos;
using Hubs;

namespace MyApplication
{
    class Program
    {
        static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Configuration.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

            var connectionString = "Server=127.0.0.1;Port=3306;Database=chatdb;Uid=root;Pwd=652006;SslMode=None;AllowPublicKeyRetrieval=True;";
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));

            builder.Services.Configure<JwtSettings>(builder.Configuration.GetSection("JwtSettings"));
            // above get config jwtsetting in json -> for oject jwtsetting class 

            builder.Services.AddScoped<IAuthService, AuthService>();
            builder.Services.AddScoped<ITokenService, TokenService>();
            builder.Services.AddScoped<IMessageService,MessageService>();

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowAll", policy =>
                {
                    policy.AllowAnyHeader()
                        .AllowAnyMethod()
                        .AllowCredentials() // Bắt buộc phải có khi dùng SignalR
                        .SetIsOriginAllowed(_ => true);
                });
            });

            builder.Services.AddSignalR(); // dang ki SignarR


            var app = builder.Build(); // đọc các câu hình


            app.UseCors("AllowAll");
            // Thêm dòng này để định nghĩa đường dẫn kết nối SignalR cho Client
            app.MapHub<ChatHubs>("/chathub"); // anh xa 

            // if (app.Environment.IsDevelopment()) // cho phép run ở môi trường dev ( đang phát triển )
            // {
            app.UseSwagger();
            app.UseSwaggerUI();
            //}
            // config pipeline 
            app.UseHttpsRedirection(); // đổi http request sang https
            app.UseMiddleware<ErrorException>();
            app.UseAuthentication();  // xác thực người dùng login
            app.UseAuthorization(); // xác thực roles, quyền hạn của người dùng
            app.MapControllers(); // ánh xạ các controller
            app.Run();
        }
    }
}

