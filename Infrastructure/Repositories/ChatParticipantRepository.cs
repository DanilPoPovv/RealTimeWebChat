using Microsoft.EntityFrameworkCore;
using RealTimeWebChat.Application.Services.Participant;
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
        public async Task<ChatParticipant> GetParticipantAsync(int chatId, int userId)
        {
            return await dbContext.Participants
                .FirstOrDefaultAsync(x => x.ChatId == chatId && x.UserId == userId);       
        }
    }
}
