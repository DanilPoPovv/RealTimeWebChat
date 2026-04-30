using RealTimeWebChat.Domain;

namespace RealTimeWebChat.Application.Services.ChatServices
{
    public interface IChatRepository
    {
        public Task AddChatAsync(Chat chat);
        public Task DeleteChatAsync(Chat chat);
        public Task<Chat> GetChatByIdAsync(int id);
        public Task UpdateAsync();
        public Task<List<Chat>> GetAllUserChatById(int userId);
        public Task<List<Chat>> SearchChatAsync(string chatName);
    }
}
