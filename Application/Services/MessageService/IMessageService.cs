using RealTimeWebChat.Presentation.Requests.Message;
using RealTimeWebChat.Presentation.Response.Message;

namespace RealTimeWebChat.Application.Services.MessageService
{
    public interface IMessageService
    {
        public Task<MessageDto> SendMessageAsync(SendMessageRequest request);
        public Task UpdateMessageAsync(UpdateMessageRequest request);
        public Task DeleteMessageAsync(DeleteMessageRequest request);
        public Task<List<MessageDto>> GetLastChatMessagesAsync(int chatId, int messageCount, int pageCount);
    }
}
