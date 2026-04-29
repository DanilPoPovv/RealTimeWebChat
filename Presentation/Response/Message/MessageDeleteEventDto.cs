

namespace RealTimeWebChat.Presentation.Response.Message
{
    public class MessageDeleteEventDto
    {
        public MessageDeleteEventDto(int ChatId, int MessageId)
        {
            this.ChatId = ChatId;
            this.MessageId = MessageId;
        }
        public int ChatId { get; set; }
        public int MessageId { get; set; }
    }
}
