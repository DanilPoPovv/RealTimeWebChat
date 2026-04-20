namespace RealTimeWebChat.Presentation.Requests
{
    public class UpdateUserRequest
    {
        public int Id { get; set; } 
        public string Name { get; set; }
        public string OldPassword { get; set; }
        public string NewPassword { get; set; }
    }
}
