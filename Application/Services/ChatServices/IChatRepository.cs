namespace RealTimeWebChat.Application.Services.ChatServices
{
    public interface IChatRepository
    {
        public Task AddChatAsync(Chat chat);
        public Task DeleteChatAsync(Chat chat);
        public Task<Chat> GetChatByIdAsync(int id);
    }
}
