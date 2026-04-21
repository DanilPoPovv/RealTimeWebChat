using Microsoft.EntityFrameworkCore;
using RealTimeWebChat.Domain;
using RealTimeWebChat.Infrastructure.Persistence;

namespace RealTimeWebChat.Infrastructure.Repositories
{
    public class ChatParticipantRepository : IChatParticipantRepository
    {
        private readonly AppDbContext dbContext;
        public ChatParticipantRepository(AppDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<Role> GetRoleAsync(int chatId, int userId)
        {
            return await dbContext.Participants.
                Where(x => x.ChatId == chatId && x.UserId == userId).
                Select(x => x.Role)
                .FirstOrDefaultAsync();
        }
    }
}
