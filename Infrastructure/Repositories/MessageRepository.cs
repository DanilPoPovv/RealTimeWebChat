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
        public async Task<List<Message>> GetLastChatMessagesAsync(int chatId, int messageCount)
        {
            return await dbContext.Messages
                .Where(m => m.ChatId == chatId)
                .OrderByDescending(m => m.CreatedAt)
                .Take(messageCount)
                .Include(m => m.User)
                .OrderBy(m => m.CreatedAt)
                .ToListAsync();
        }
    }
}
