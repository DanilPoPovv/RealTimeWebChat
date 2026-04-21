using Azure.Core;
using RealTimeWebChat.Application.Services.ChatServices;
using RealTimeWebChat.Application.Services.UserServices;
using RealTimeWebChat.Domain;
using RealTimeWebChat.Infrastructure.Repositories;
using RealTimeWebChat.Presentation.Requests.Message;
using RealTimeWebChat.Presentation.Response.Message;
using RealTimeWebChat.Presentation.Response.User;

namespace RealTimeWebChat.Application.Services.MessageService
{
    public class MessageService : IMessageService
    {
        private readonly IMessageRepository messageRepository;
        private readonly IChatRepository chatRepository;
        private readonly IChatParticipantRepository chatParticipantRepository;
        private readonly IUserRepository userRepository;
         public MessageService(
            IMessageRepository messageRepository,
            IChatRepository chatRepository, 
            IChatParticipantRepository chatParticipant,
            IUserRepository userRepository)
        {
            this.messageRepository = messageRepository;
            this.chatRepository = chatRepository;
            this.chatParticipant = chatParticipant;
            this.userRepository = userRepository;
        }
        public async Task DeleteMessageAsync(DeleteMessageRequest request)
        {
            var message = await messageRepository.GetMessageAsync(request.MessageId);
            var user = await userRepository.GetByIdAsync(request.UserId);
            var participant = await 
            if (message == null)
                throw new Exception("Message not found");
            if (user == null)
                throw new Exception("User not found");
            var chat = await chatRepository.GetChatByIdAsync(request.ChatId);
            if (chat == null)
                throw new Exception("Chat not found");

            if (message.ChatId != request.ChatId)
                throw new Exception("Message does not belong to this chat");

            if (userRole == null)
                throw new Exception("User is not a participant of the chat");

            var isOwner = request.UserId == message.UserId;
            var isAdmin = userRole == Role.Admin || userRole == Role.SuperAdmin;

            if (!isOwner && !isAdmin)
                throw new Exception("You can't delete other user's message");

            await messageRepository.DeleteMessageAsync(message);
        }

        public async Task<List<MessageDto>> GetLastChatMessagesAsync(int chatId, int messageCount, int pageCount)
        {
            var messages = await messageRepository
                .GetLastChatMessagesAsync(chatId, messageCount, pageCount);

            return messages.Select(m => new MessageDto
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

        public async Task<MessageDto> SendMessageAsync(SendMessageRequest request)
        {
            var user = await userRepository.GetByIdAsync(request.SenderId);
            var chat = await chatRepository.GetChatByIdAsync(request.ChatId);
            if (user == null)
                throw new Exception("User not found");
            if (chat == null)
                throw new Exception("Chat not found");
            var message = new Message()
            {
                Text = request.Text,
                UserId = request.SenderId,
                CreatedAt = DateTime.UtcNow,
                ChatId = request.ChatId
            };

            await messageRepository.AddMessageAsync(message);

            return new MessageDto()
            {
                Id = message.Id,
                Text = message.Text,
                CreatedAt = message.CreatedAt,
                User = new UserDto()
                {
                    Id = user.Id,
                    Name = user.Name
                }
            };
        }

        public async Task UpdateMessageAsync(UpdateMessageRequest request)
        {
            var message = await messageRepository.GetMessageAsync(request.MessageId);
            if (message.UserId != request.UserId)
                throw new Exception("You can't change other user message");
            if (!string.IsNullOrWhiteSpace(request.Text))
                message.Text = request.Text;
            await messageRepository.UpdateMessageAsync();

        }
    }
}
