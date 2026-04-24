using RealTimeWebChat.Presentation.Requests.Message;
using RealTimeWebChat.Presentation.Response.Message;

public interface IMessageService
{
    Task<MessageReceivedEventDto> SendMessageAsync(int userId, SendMessageRequest request);

    Task<MessageUpdateEventDto> UpdateMessageAsync(int userId, UpdateMessageRequest request);

    Task<MessageDeleteEventDto> DeleteMessageAsync(int userId, DeleteMessageRequest request);

    Task<List<MessageReceivedEventDto>> GetLastChatMessagesAsync(int userId, int chatId, int messageCount, int pageCount);
}