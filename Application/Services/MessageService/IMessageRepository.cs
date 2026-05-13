namespace RealTimeWebChat.Application.Services.MessageService
{
    public interface IMessageRepository
    {
        public Task<List<Message>> GetChatMessagesAsync(int chatId, int limit, int? beforeMessageId);
        public Task DeleteMessageAsync(Message message);
        public Task<Message> GetMessageAsync(int id);
        public Task UpdateMessageAsync();
        public Task AddMessageAsync(Message message);
    }
}
