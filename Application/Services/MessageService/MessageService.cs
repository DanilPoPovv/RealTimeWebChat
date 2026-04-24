using RealTimeWebChat.Application.Services.ChatServices;
using RealTimeWebChat.Application.Services.MessageService;
using RealTimeWebChat.Application.Services.UserServices;
using RealTimeWebChat.Domain;
using RealTimeWebChat.Infrastructure.Repositories;
using RealTimeWebChat.Presentation.Requests.Message;
using RealTimeWebChat.Presentation.Response.Message;
using RealTimeWebChat.Presentation.Response.User;

public class MessageService : IMessageService
{
    private readonly IMessageRepository messageRepository;
    private readonly IChatRepository chatRepository;
    private readonly IParticipantRepository participantRepository;
    private readonly IUserRepository userRepository;

    public MessageService(
        IMessageRepository messageRepository,
        IChatRepository chatRepository,
        IParticipantRepository participantRepository,
        IUserRepository userRepository)
    {
        this.messageRepository = messageRepository;
        this.chatRepository = chatRepository;
        this.participantRepository = participantRepository;
        this.userRepository = userRepository;
    }

    public async Task<MessageReceivedEventDto> SendMessageAsync(int userId, SendMessageRequest request)
    {
        var chat = await chatRepository.GetChatByIdAsync(request.ChatId);
        if (chat == null)
            throw new Exception("Chat not found");

        var participant = await participantRepository.GetParticipantAsync(chat.Id, userId);
        if (participant == null)
            throw new Exception("User not in chat");

        var user = await userRepository.GetByIdAsync(userId);

        var message = new Message
        {
            Text = request.Text,
            UserId = userId,
            ChatId = request.ChatId,
            CreatedAt = DateTime.UtcNow
        };

        await messageRepository.AddMessageAsync(message);

        return new MessageReceivedEventDto
        {
            Id = message.Id,
            Text = message.Text,
            CreatedAt = message.CreatedAt,
            ChatId = message.ChatId,
            User = new UserDto
            {
                Id = user.Id,
                Name = user.Name
            }
        };
    }

    public async Task<MessageDeleteEventDto> DeleteMessageAsync(int userId, DeleteMessageRequest request)
    {
        var message = await messageRepository.GetMessageAsync(request.MessageId);

        if (message == null)
            throw new Exception("Message not found");

        if (message.ChatId != request.ChatId)
            throw new Exception("Wrong chat");

        var participant = await participantRepository.GetParticipantAsync(message.ChatId, userId);

        if (participant == null)
            throw new Exception("Not participant");

        var isOwner = message.UserId == userId;
        var isAdmin = participant.Role == Role.Admin || participant.Role == Role.SuperAdmin;

        if (!isOwner && !isAdmin)
            throw new Exception("No permission");

        await messageRepository.DeleteMessageAsync(message);
        return new MessageDeleteEventDto(
            message.ChatId,
            message.Id);
    }

    public async Task<MessageUpdateEventDto> UpdateMessageAsync(int userId, UpdateMessageRequest request)
    {
        var message = await messageRepository.GetMessageAsync(request.MessageId);

        if (message == null)
            throw new Exception("Message not found");

        if (message.UserId != userId)
            throw new Exception("No permission");

        if (!string.IsNullOrWhiteSpace(request.Text))
            message.Text = request.Text;

        await messageRepository.UpdateMessageAsync();
        return new MessageUpdateEventDto(
            message.Text,
            message.Id,
            message.ChatId);

    }

    public async Task<List<MessageReceivedEventDto>> GetLastChatMessagesAsync(
        int userId,
        int chatId,
        int messageCount,
        int pageCount)
    {
        var participant = await participantRepository.GetParticipantAsync(chatId, userId);

        if (participant == null)
            throw new Exception("Access denied");

        var messages = await messageRepository
            .GetLastChatMessagesAsync(chatId, messageCount, pageCount);

        return messages.Select(m => new MessageReceivedEventDto
        {
            Id = m.Id,
            Text = m.Text,
            CreatedAt = m.CreatedAt,
            ChatId = m.ChatId,
            User = new UserDto
            {
                Id = m.User.Id,
                Name = m.User.Name
            }
        }).ToList();
    }
}