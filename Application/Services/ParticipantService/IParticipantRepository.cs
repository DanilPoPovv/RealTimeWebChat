
namespace RealTimeWebChat.Infrastructure.Repositories
{
    public interface IParticipantRepository
    {
        public Task<ChatParticipant> GetParticipantAsync(int chatId, int userId);
        public Task AddParticipantAsync(ChatParticipant participant);
        public Task DeleteParticipantAsync(ChatParticipant participant);

    }
}
