namespace RealTimeWebChat.Application.Services.JwtService
{
    public interface IJwtService
    {
        public string GenerateToken(User user);
    }
}
