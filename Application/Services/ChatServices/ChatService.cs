using RealTimeWebChat.Application.Services.ChatServices;
using RealTimeWebChat.Application.Services.UserServices;
using RealTimeWebChat.Domain;
using RealTimeWebChat.Presentation.Requests.Chat;
using RealTimeWebChat.Presentation.Response.Chat;

namespace RealTimeWebChat.Application.Services.ChatServices
{
    public class ChatService : IChatService
    {
        private readonly IChatRepository chatRepository;
        private readonly IUserRepository userRepository;
        public ChatService(IUserRepository userRepository, IChatRepository chatRepository)
        {
            this.chatRepository = chatRepository;
            this.userRepository = userRepository;
        }
        public async Task<CreateChatResponse> CreateChatAsync(CreateChatRequest request)
        {
            var chat = new Chat()
            {
                Name = request.Name,
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

        public Task<GetChatResponse> GetChatAsync(GetChatRequest request)
        {
            var Chat = await chatRepository.AddChatAsync(request.Id);
        }

        public Task<UpdateChatResponse> UpdateChatAsync(UpdateChatRequest request)
        {
            throw new NotImplementedException();
        }
    }
}
