using RealTimeWebChat.Presentation.Requests.Chat;
using RealTimeWebChat.Presentation.Response.Chat;

namespace RealTimeWebChat.Application.Services.ChatServices
{
    public interface IChatService
    {
        public Task<GetChatResponse> GetChatAsync(GetChatRequest request);
        public Task<UpdateChatResponse> UpdateChatAsync(UpdateChatRequest request);
        public Task DeleteChatAsync(DeleteChatRequest request);
        public Task<CreateChatResponse> CreateChatAsync(CreateChatRequest request);
    }
}
