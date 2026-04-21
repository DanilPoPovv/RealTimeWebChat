using RealTimeWebChat.Application.Services.ChatServices;
using RealTimeWebChat.Application.Services.MessageService;
using RealTimeWebChat.Application.Services.UserServices;
using RealTimeWebChat.Domain;
using RealTimeWebChat.Presentation.Requests.Chat;
using RealTimeWebChat.Presentation.Response.Chat;
using RealTimeWebChat.Presentation.Response.Message;
using RealTimeWebChat.Presentation.Response.User;

namespace RealTimeWebChat.Application.Services.ChatServices
{
    public class ChatService : IChatService
    {
        private readonly IChatRepository chatRepository;
        private readonly IUserRepository userRepository;
        private readonly IMessageRepository messageRepository;
        public ChatService
            (IUserRepository userRepository,
             IChatRepository chatRepository,
             IMessageRepository messageRepository)
        {
            this.chatRepository = chatRepository;
            this.userRepository = userRepository;
            this.messageRepository = messageRepository;
        }
        public async Task<CreateChatResponse> CreateChatAsync(CreateChatRequest request)
        {
            var chat = new Chat()
            {
                Name = request.Name,
                Participants = new List<ChatParticipant>(1)
            };
            var superAdmin = await userRepository.GetByIdAsync(request.CreatorId);
            if (superAdmin == null)
                throw new Exception("User not found");
            chat.Participants.Add(new ChatParticipant()
            {
                User = superAdmin,
                Role = Role.SuperAdmin
            });
            await chatRepository.AddChatAsync(chat);
            return new CreateChatResponse
            {
                Id = chat.Id,
                Name = chat.Name
            };
        }

        public async Task DeleteChatAsync(DeleteChatRequest request)
        {
            var Chat = await chatRepository.GetChatByIdAsync(request.Id);
            if (Chat == null) throw new Exception("Chat not found");
            await chatRepository.DeleteChatAsync(Chat);
        }

        public async Task<GetChatResponse> GetChatAsync(GetChatRequest request)
        {
            var Chat = await chatRepository.GetChatByIdAsync(request.Id);
            if (Chat == null)
                throw new Exception("Chat not found");
            List<MessageDto> lastMessages = null;
            if (request.LastMessages != null)
            {
                List<Message> messages = await messageRepository.GetLastChatMessagesAsync(Chat.Id, request.LastMessages.Value);

                lastMessages = messages.Select(m => new MessageDto
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
            return new GetChatResponse()
            {
                Id = Chat.Id,
                Name = Chat.Name,
                Messages = lastMessages
            };
        }

        public async Task<UpdateChatResponse> UpdateChatAsync(UpdateChatRequest request)
        {
            var Chat = await chatRepository.GetChatByIdAsync(request.Id);
            if (!string.IsNullOrWhiteSpace(request.Name))
                Chat.Name = request.Name;
            await chatRepository.UpdateAsync();
            return new UpdateChatResponse()
            {
                Id = Chat.Id,
                Name = Chat.Name
            };
        }
    }
}
