using Microsoft.EntityFrameworkCore;
using RealTimeWebChat.Application.Services.UserLayer;
using RealTimeWebChat.Infrastructure.Persistence;

namespace RealTimeWebChat.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext dbContext;
        public UserRepository(AppDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task AddAsync(User user)
        {
            await dbContext.AddAsync(user);
            await dbContext.SaveChangesAsync();
        }

        public async Task SoftDeleteAsync(int id)
        {
            var user = await GetByIdAsync(id);
            if (user == null)
                throw new Exception("User not found");

            user.IsDeleted = true;

            await dbContext.SaveChangesAsync();
        }

        public async Task<User> GetByNameAsync(string userName)
        {
            return await dbContext.Users.
                FirstOrDefaultAsync(u => u.Name == userName);
        }

        public async Task SaveChangesAsync()
        {
            await dbContext.SaveChangesAsync();
        }
        public async Task<User> GetByIdAsync(int id)
        {
            return await dbContext.Users.
            FirstOrDefaultAsync(u => u.Id == id);
        }
    }
}
