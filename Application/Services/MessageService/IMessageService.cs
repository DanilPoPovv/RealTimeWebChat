using RealTimeWebChat.Presentation.Requests.Message;
using RealTimeWebChat.Presentation.Response.Message;

public interface IMessageService
{
    Task<MessageDto> SendMessageAsync(int userId, SendMessageRequest request);

    Task UpdateMessageAsync(int userId, UpdateMessageRequest request);

    Task DeleteMessageAsync(int userId, DeleteMessageRequest request);

    Task<List<MessageDto>> GetLastChatMessagesAsync(int userId, int chatId, int messageCount, int pageCount);
}