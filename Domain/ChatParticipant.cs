
using RealTimeWebChat.Domain;

public class ChatParticipant
{
    public int UserId { get; set; }
    public User User { get; set; }

    public int ChatId { get; set; }
    public Chat Chat { get; set; }

    public Role Role { get; set; }
}