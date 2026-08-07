
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi;
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
            builder.Services.AddScoped<IMessageService, MessageService>();

            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = builder.Configuration["JwtSettings:Issuer"],
                        ValidAudience = builder.Configuration["JwtSettings:Audience"],
                        IssuerSigningKey = new SymmetricSecurityKey(
                            Encoding.UTF8.GetBytes(builder.Configuration["JwtSettings:SecretKey"]!))
                    };
                });

            builder.Services.AddAuthorization();
            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Chat API", Version = "v1" });
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    Scheme = "bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "Nhập token JWT theo định dạng: Bearer {token}"
                });
                c.AddSecurityRequirement(doc => new OpenApiSecurityRequirement
                {
                    [new OpenApiSecuritySchemeReference("Bearer", doc, "Bearer")] = new List<string>()
                });
            });
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

