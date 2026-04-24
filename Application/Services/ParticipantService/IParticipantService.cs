using RealTimeWebChat.Presentation.Response.Participant;

namespace RealTimeWebChat.Application.Services.Participant
{
    public interface IParticipantService
    {
        public Task<UserJoinedChatDto> JoinChatAsync(int userId, int chatId);
        public Task<UserLeftChatDto> LeaveChatAsync(int userId, int chatId);
    }
}
