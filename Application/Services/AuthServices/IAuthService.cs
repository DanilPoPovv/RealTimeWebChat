using RealTimeWebChat.Presentation.Requests.Login;
using RealTimeWebChat.Presentation.Response.Auth;

namespace RealTimeWebChat.Application.Services.AuthService
{
    public interface IAuthService
    {
        public Task<LoginResponse> LoginAsync(LoginRequest request);
    }
}
