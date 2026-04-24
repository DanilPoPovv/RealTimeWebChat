using RealTimeWebChat.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using RealTimeWebChat.Application.Services.UserServices;
using RealTimeWebChat.Infrastructure.Repositories;
using Microsoft.AspNetCore.Identity;
using RealTimeWebChat.Application.Services.MessageService;
using RealTimeWebChat.Application.Services.ChatServices;
<<<<<<< HEAD
using RealTimeWebChat.Application.Services.Participant;
using RealTimeWebChat.Helpers;
=======
using RealTimeWebChat.Infrastructure.Extensions;
using RealTimeWebChat.Application.Services.AuthServices;
>>>>>>> cb2d70e82eb25df5a788c33b1f2258da0e0721d6
var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection"));
});
builder.Services.AddScoped<IUserService,UserService>();
builder.Services.AddScoped<IChatService, ChatService>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IChatRepository, ChatRepository>();
builder.Services.AddScoped<IMessageRepository, MessageRepository>();
builder.Services.AddScoped<IPasswordHasher<User>, PasswordHasher<User>>();
builder.Services.AddScoped<IMessageRepository, MessageRepository>();
builder.Services.AddScoped<IMessageService, MessageService>();
builder.Services.AddScoped<IChatParticipantRepository, ChatParticipantRepository>();
<<<<<<< HEAD
builder.AddBearerAuth();
=======
builder.Services.AddScoped<IJwtService, JwtService>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.AddJwtAuth();
>>>>>>> cb2d70e82eb25df5a788c33b1f2258da0e0721d6
var app = builder.Build();
app.UseMiddleware<ExceptionMiddleware>();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
