namespace RealTimeWebChat.Presentation.Requests.Message
{
    public class UpdateMessageRequest
    {
        public int MessageId { get; set; }
        public string Text { get; set; }
        public int UserId { get; set; }
    }
}
