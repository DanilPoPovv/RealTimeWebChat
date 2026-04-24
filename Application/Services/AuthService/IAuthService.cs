using RealTimeWebChat.Presentation.Requests.Login;

namespace RealTimeWebChat.Application.Services.AuthService
{
    public interface IAuthService
    {
        public Task<string> LoginAsync(LoginRequest request);
    }
}
