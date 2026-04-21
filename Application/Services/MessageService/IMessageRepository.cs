namespace RealTimeWebChat.Application.Services.MessageService
{
    public interface IMessageRepository
    {
        public Task<List<Message>> GetLastChatMessagesAsync(int chatId, int messageCount);
    }
}
