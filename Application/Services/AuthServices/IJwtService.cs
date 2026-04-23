namespace RealTimeWebChat.Application.Services.AuthServices
{
    public interface IJwtService
    {
        public string GenerateToken(User user);
    }
}
