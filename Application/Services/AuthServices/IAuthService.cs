using RealTimeWebChat.Presentation.Requests.Login;

namespace RealTimeWebChat.Application.Services.AuthServices
{
    public interface IAuthService
    {
        public Task<string> LoginAsync(LoginRequest request);
    }
}
