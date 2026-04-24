using RealTimeWebChat.Domain;

namespace RealTimeWebChat.Infrastructure.Repositories
{
    public interface IParticipantRepository
    {
        public Task<ChatParticipant> GetParticipantAsync(int chatId, int userId);
    }
}
