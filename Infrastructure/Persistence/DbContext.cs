using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography.X509Certificates;

namespace RealTimeWebChat.Infrastructure.Persistence
{
    public class AppDbContext : DbContext
    {
        public override void OnModelCreating(ModelBuilder modelBuilder)
        {
            
        }
    }
}
