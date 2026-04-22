using RealTimeWebChat.Domain;

namespace RealTimeWebChat.Infrastructure.Repositories
{
    public interface IChatParticipantRepository
    {
        public Task<ChatParticipant> GetParticipantAsync(int chatId, int userId);
    }
}
