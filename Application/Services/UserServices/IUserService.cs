using RealTimeWebChat.Presentation.Requests;
using RealTimeWebChat.Presentation.Requests.User;
using RealTimeWebChat.Presentation.Response.User;
using RealTimeWebChat.Presentation.Responses.User;

namespace RealTimeWebChat.Application.Services.UserServices
{
    public interface IUserService
    {
        public Task<CreateUserResponse> CreateUserAsync(CreateUserRequest request);

        public Task SoftDeleteUserAsync(DeleteUserRequest request);

        public Task<UpdateUserResponse> UpdateUserAsync(UpdateUserRequest request);

        public Task<UserDto> GetUserById(int userId);
        public Task<AvatarUploadEvent> UploadAvatar(IFormFile avatar, int userId);
    }
}
