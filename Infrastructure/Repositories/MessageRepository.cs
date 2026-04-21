using Microsoft.EntityFrameworkCore;
using RealTimeWebChat.Application.Services.MessageService;
using RealTimeWebChat.Infrastructure.Persistence;
using System.Collections.Generic;

namespace RealTimeWebChat.Infrastructure.Repositories
{
    public class MessageRepository : IMessageRepository
    {
        private readonly AppDbContext dbContext;
        public MessageRepository(AppDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<Message> GetMessageAsync(int id)
        {
            return await dbContext.Messages.FirstOrDefaultAsync(x => x.Id == id);
        }
        public async Task AddMessageAsync(Message message)
        {
            await dbContext.AddAsync(message);
            await dbContext.SaveChangesAsync();
        }

        public async Task DeleteMessageAsync(Message message)
        {
            dbContext.Remove(message);
            await dbContext.SaveChangesAsync();
        }

        public async Task<List<Message>> GetLastChatMessagesAsync(int chatId, int messageCount, int pageCount = 0)
        {
            return await dbContext.Messages
                .Where(m => m.ChatId == chatId)
                .OrderByDescending(m => m.CreatedAt)
                .Skip(pageCount * messageCount)
                .Take(messageCount)
                .Include(m => m.User)
                .OrderBy(m => m.CreatedAt)
                .ToListAsync();
        }

        public async Task UpdateMessageAsync()
        {
            await dbContext.SaveChangesAsync();
        }
    }
}
