using RealTimeWebChat.Presentation.Response.User;

namespace RealTimeWebChat.Presentation.Response.Message
{
    public class MessageDto
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public DateTime CreatedAt { get; set; }
        public UserDto User { get; set; }
    }
}
