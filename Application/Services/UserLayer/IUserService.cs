using RealTimeWebChat.Presentation.Requests;
using RealTimeWebChat.Presentation.Requests.User;
using RealTimeWebChat.Presentation.Responses.User;

namespace RealTimeWebChat.Application.Services.UserLayer
{
    public interface IUserService
    {
        public Task<CreateUserResponse> CreateUserAsync(CreateUserRequest request);

        public Task SoftDeleteUserAsync(DeleteUserRequest request);

        public Task<UpdateUserResponse> UpdateUserAsync(UpdateUserRequest request);

        public Task<GetUserResponse> GetUserByNameAsync(GetUserRequest request);
    }
}
