using RealTimeWebChat.Application.Services.ChatServices;
using RealTimeWebChat.Application.Services.Participant;
using RealTimeWebChat.Application.Services.UserServices;
using RealTimeWebChat.Domain;
using RealTimeWebChat.Infrastructure.Repositories;
using RealTimeWebChat.Presentation.Response.Participant;

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

    public async Task<UserJoinedChatDto> JoinChatAsync(int userId, int chatId)
    {
        var user = await userRepository.GetByIdAsync(userId);
        var chat = await chatRepository.GetChatByIdAsync(chatId);

        if (user == null) throw new Exception("user not found");
        if (chat == null) throw new Exception("chat not found");

        var exists = await participantRepository.GetParticipantAsync(chatId, userId);
        if (exists != null) throw new Exception("already in chat");

        await participantRepository.AddParticipantAsync(new ChatParticipant
        {
            ChatId = chatId,
            UserId = userId,
            Role = Role.Member
        });

        return new UserJoinedChatDto(user.Name, user.Id, chat.Id);
    }

    public async Task<UserLeftChatDto> LeaveChatAsync(int userId, int chatId)
    {
        var participant = await participantRepository.GetParticipantAsync(chatId, userId);
        if (participant == null) throw new Exception("not in chat");

        await participantRepository.DeleteParticipantAsync(participant);

        var user = await userRepository.GetByIdAsync(userId);

        return new UserLeftChatDto(user.Name, user.Id, chatId);
    }
}