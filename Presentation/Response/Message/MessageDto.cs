using RealTimeWebChat.Presentation.Response.User;

namespace RealTimeWebChat.Presentation.Response.Message
{
    public class MessageDto
    {
        public string Text { get; set; }
        public UserDto User { get; set; }
    }
}
