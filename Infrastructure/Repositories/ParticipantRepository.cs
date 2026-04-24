using Microsoft.EntityFrameworkCore;
using RealTimeWebChat.Infrastructure.Persistence;

namespace RealTimeWebChat.Infrastructure.Repositories
{
    public class ParticipantRepository : IParticipantRepository
    {
        private readonly AppDbContext dbContext;
        public ParticipantRepository(AppDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task AddParticipantAsync(ChatParticipant participant)
        {
            await dbContext.Participants.AddAsync(participant);
            await dbContext.SaveChangesAsync();
        }

        public async Task DeleteParticipantAsync(ChatParticipant participant)
        {
             dbContext.Participants.Remove(participant);
             await dbContext.SaveChangesAsync();
        }

        public async Task<ChatParticipant> GetParticipantAsync(int chatId, int userId)
        {
            return await dbContext.Participants
                .FirstOrDefaultAsync(x => x.ChatId == chatId && x.UserId == userId);       
        }
    }
}
