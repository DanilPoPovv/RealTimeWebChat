using RealTimeWebChat.Presentation.Response.User;

namespace RealTimeWebChat.Presentation.Response.Message
{
    public class MessageReceivedEventDto
    {
        public int Id { get; set; }
        public int ChatId { get; set; }
        public string Text { get; set; }
        public DateTime CreatedAt { get; set; }
        public UserDto User { get; set; }
    }
}
