using RealTimeWebChat.Presentation.Requests.Chat;
using RealTimeWebChat.Presentation.Response.Chat;

public interface IChatService
{
    Task<CreateChatResponse> CreateChatAsync(int userId, CreateChatRequest request);

    Task DeleteChatAsync(int userId, DeleteChatRequest request);

    Task<GetChatResponse> GetChatAsync(int userId, GetChatRequest request);

    Task<UpdateChatResponse> UpdateChatAsync(int userId, UpdateChatRequest request);
    Task<List<ChatDto>> GetAllUserChats(int userId);
}