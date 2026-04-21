using RealTimeWebChat.Presentation.Response.Message;

namespace RealTimeWebChat.Presentation.Requests.Chat
{
    public class GetChatRequest
    {
        public int Id {get; set;}
        public int? LastMessages { get; set;}
    } 
}
