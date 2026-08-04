
using Microsoft.EntityFrameworkCore;
using Source.Data;
using Source.Services;

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
            builder.Services.AddScoped<IAuthService,AuthService>();

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            var app=builder.Build(); // đọc các câu hình
            if (app.Environment.IsDevelopment()) // cho phép run ở môi trường dev ( đang phát triển )
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            // config pipeline 
            app.UseHttpsRedirection(); // đổi http request sang https
            app.UseAuthentication();  // xác thực người dùng login
            app.UseAuthorization(); // xác thực roles, quyền hạn của người dùng
            app.MapControllers(); // ánh xạ các controller
            app.Run();
        }
    }
}

