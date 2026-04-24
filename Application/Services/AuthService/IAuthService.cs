using Microsoft.AspNetCore.Identity.Data;

namespace RealTimeWebChat.Application.Services.AuthService
{
    public interface IAuthService
    {
        public Task<string> LoginAsync(LoginRequest request);
    }
}
