namespace RealTimeWebChat.Presentation.Requests.Message
{
    public class DeleteMessageRequest
    {
        public int MessageId { get; set; }
        public int UserId { get; set; }
        public int ChatId { get; set; }
    }
}
