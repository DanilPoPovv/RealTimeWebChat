using RealTimeWebChat.Presentation.Response.Message;

namespace RealTimeWebChat.Presentation.Response.Chat
{
    public class GetChatResponse
    {
        public int Id{get; set;}
        public string Name{get; set;}
        public List<MessageReceivedEventDto>? Messages { get; set; }
    } 
}
