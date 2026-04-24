namespace RealTimeWebChat.Application.Services.Participant
{
    public interface IParticipantService
    {
        public Task JoinChatAsync(int userId, int chatId);
        public Task LeaveChatAsync(int userId, int chatId);
    }
}
