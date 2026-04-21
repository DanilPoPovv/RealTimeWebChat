using RealTimeWebChat.Domain;

namespace RealTimeWebChat.Infrastructure.Repositories
{
    public interface IChatParticipantRepository
    {
        public Task<Role> GetRoleAsync(int chatId, int userId);
    }
}
