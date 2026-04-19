public class User
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string PasswordHash { get; set; }
    public bool IsDeleted { get; set; }
    public List<ChatParticipant> Chats { get; set; }
}