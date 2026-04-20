
namespace RealTimeWebChat.Application.Services.UserServices
{
    public interface IUserRepository
    {
        public Task AddAsync(User user);
        public Task<User> GetByNameAsync(string userName);
        public Task SaveChangesAsync();
        public Task SoftDeleteAsync(int id);
        public Task<User> GetByIdAsync(int id);
    }
}
