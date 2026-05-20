using RealTimeWebChat.Presentation.Response.User;

namespace RealTimeWebChat.Presentation.Response.Auth
{
    public record class LoginResponse(string token, UserDto userDto);
}
