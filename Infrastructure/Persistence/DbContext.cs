using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography.X509Certificates;

namespace RealTimeWebChat.Infrastructure.Persistence
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
    : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Chat> Chats { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<ChatParticipant> Participants { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ChatParticipant>()
                .HasKey(x => new { x.ChatId, x.UserId });

            modelBuilder.Entity<ChatParticipant>()
                .HasOne(x => x.Chat)
                .WithMany(x => x.Participants)
                .HasForeignKey(x => x.ChatId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<ChatParticipant>()
                .HasOne(x => x.User)
                .WithMany(x => x.Chats)
                .HasForeignKey(x => x.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Message>()
                .HasOne(x => x.User)
                .WithMany()
                .HasForeignKey(x => x.UserId)
                .OnDelete(DeleteBehavior.Restrict); 

            modelBuilder.Entity<Message>()
                .HasOne(x => x.Chat)
                .WithMany(x => x.Messages)
                .HasForeignKey(x => x.ChatId);
        }
    }
}
