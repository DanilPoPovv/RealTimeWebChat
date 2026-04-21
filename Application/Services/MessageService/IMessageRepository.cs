namespace RealTimeWebChat.Application.Services.MessageService
{
    public interface IMessageRepository
    {
        public Task<List<Message>> GetLastChatMessagesAsync(int chatId, int messageCount, int pageCount = 0);
        public Task DeleteMessageAsync(Message message);
        public Task<Message> GetMessageAsync(int id);
        public Task UpdateMessageAsync();
        public Task AddMessageAsync(Message message);
    }
}
