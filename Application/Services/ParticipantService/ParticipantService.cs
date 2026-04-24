using RealTimeWebChat.Application.Services.ChatServices;
using RealTimeWebChat.Application.Services.UserServices;
using RealTimeWebChat.Domain;
using RealTimeWebChat.Infrastructure.Repositories;

namespace RealTimeWebChat.Application.Services.Participant
{
    public class ParticipantService : IParticipantService
    {
        private readonly IChatRepository chatRepository;
        private readonly IParticipantRepository participantRepository;
        private readonly IUserRepository userRepository;

        public ParticipantService(
            IChatRepository chatRepository,
            IParticipantRepository participantRepository,
            IUserRepository userRepository)
        {
            this.chatRepository = chatRepository;
            this.participantRepository = participantRepository;
            this.userRepository = userRepository;
        }
        public async Task JoinChatAsync(int userId, int chatId)
        {
            var user = await userRepository.GetByIdAsync(userId);
            if (user == null)
                throw new Exception("user not found");
            var chat = await chatRepository.GetChatByIdAsync(chatId);
            if (chat == null)
                throw new Exception("chat not found");
            var existing = await participantRepository.GetParticipantAsync(chat.Id, user.Id);
            if (existing != null)
                throw new Exception("User already in chat");
            var participant = new ChatParticipant()
            {
                ChatId = chat.Id,
                UserId = user.Id,
                Role = Role.Member
            };
            await participantRepository.AddParticipantAsync(participant);
        }

        public async Task LeaveChatAsync(int userId, int chatId)
        {
            var participant = await participantRepository.GetParticipantAsync(chatId, userId);
            if (participant == null)
                throw new Exception("user is not a participant of this chat");
            await participantRepository.DeleteParticipantAsync(participant);
        }
    }
}
