using RealTimeWebChat.Application.Services.ChatServices;
using RealTimeWebChat.Application.Services.MessageService;
using RealTimeWebChat.Application.Services.UserServices;
using RealTimeWebChat.Domain;
using RealTimeWebChat.Infrastructure.Repositories;
using RealTimeWebChat.Presentation.Requests.Chat;
using RealTimeWebChat.Presentation.Response.Chat;
using RealTimeWebChat.Presentation.Response.Message;
using RealTimeWebChat.Presentation.Response.User;

public class ChatService : IChatService
{
    private readonly IChatRepository chatRepository;
    private readonly IParticipantRepository participantRepository;
    private readonly IMessageRepository messageRepository;
    private readonly IUserRepository userRepository;

    public ChatService(
        IChatRepository chatRepository,
        IParticipantRepository participantRepository,
        IMessageRepository messageRepository,
        IUserRepository userRepository)
    {
        this.chatRepository = chatRepository;
        this.participantRepository = participantRepository;
        this.messageRepository = messageRepository;
        this.userRepository = userRepository;
    }

    public async Task<CreateChatResponse> CreateChatAsync(int userId, CreateChatRequest request)
    {
        var chat = new Chat
        {
            Name = request.Name,
            Participants = new List<ChatParticipant>()
        };

        chat.Participants.Add(new ChatParticipant
        {
            UserId = userId,
            Role = Role.SuperAdmin
        });

        await chatRepository.AddChatAsync(chat);

        return new CreateChatResponse
        {
            Id = chat.Id,
            Name = chat.Name
        };
    }

    public async Task DeleteChatAsync(int userId, DeleteChatRequest request)
    {
        var chat = await chatRepository.GetChatByIdAsync(request.Id);

        if (chat == null)
            throw new Exception("Chat not found");

        var participant = await participantRepository.GetParticipantAsync(chat.Id, userId);

        if (participant == null || participant.Role != Role.SuperAdmin)
            throw new Exception("No permission");

        await chatRepository.DeleteChatAsync(chat);
    }

    public async Task<GetChatResponse> GetChatAsync(int userId, GetChatRequest request)
    {
        var chat = await chatRepository.GetChatByIdAsync(request.Id);

        if (chat == null)
            throw new Exception("Chat not found");

        var participant = await participantRepository.GetParticipantAsync(chat.Id, userId);

        if (participant == null)
            throw new Exception("Access denied");

        List<MessageReceivedEventDto> messages = null;

        if (request.LastMessages != null)
        {
            var dbMessages = await messageRepository
                .GetLastChatMessagesAsync(chat.Id, request.LastMessages.Value);

            messages = dbMessages.Select(m => new MessageReceivedEventDto
            {
                Id = m.Id,
                Text = m.Text,
                CreatedAt = m.CreatedAt,
                User = new UserDto
                {
                    Id = m.User.Id,
                    Name = m.User.Name
                }
            }).ToList();
        }

        return new GetChatResponse
        {
            Id = chat.Id,
            Name = chat.Name,
            Messages = messages
        };
    }

    public async Task<UpdateChatResponse> UpdateChatAsync(int userId, UpdateChatRequest request)
    {
        var chat = await chatRepository.GetChatByIdAsync(request.Id);

        if (chat == null)
            throw new Exception("Chat not found");

        var participant = await participantRepository.GetParticipantAsync(chat.Id, userId);

        if (participant == null || participant.Role == Role.Member)
            throw new Exception("No permission");

        if (!string.IsNullOrWhiteSpace(request.Name))
            chat.Name = request.Name;

        await chatRepository.UpdateAsync();

        return new UpdateChatResponse
        {
            Id = chat.Id,
            Name = chat.Name
        };
    }
}