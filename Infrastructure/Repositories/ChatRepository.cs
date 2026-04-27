using RealTimeWebChat.Application.Services.ChatServices;
using RealTimeWebChat.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace RealTimeWebChat.Infrastructure.Repositories
{
    public class ChatRepository : IChatRepository
    {
        private readonly AppDbContext dbContext;
        public ChatRepository(AppDbContext appDbContext) {
            this.dbContext = appDbContext;
        }
        public async Task AddChatAsync(Chat chat)
        {
            await dbContext.Chats.AddAsync(chat);
            await dbContext.SaveChangesAsync();
        }
        public async Task DeleteChatAsync(Chat chat) 
        {
            dbContext.Remove(chat);
            await dbContext.SaveChangesAsync();
        }

        public async Task<List<Chat>> GetAllUserChatById(int userId)
        {
            return await dbContext.Participants
                .Where(p => p.UserId == userId)
                .Select(p => p.Chat)
                .ToListAsync();
        }

        public async Task<Chat> GetChatByIdAsync(int id)
        {
            return await dbContext.Chats.FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task UpdateAsync()
        {
            await dbContext.SaveChangesAsync();
        }
    }
}
