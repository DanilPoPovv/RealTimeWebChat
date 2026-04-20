using Microsoft.AspNetCore.Identity;
using RealTimeWebChat.Presentation.Requests;
using RealTimeWebChat.Presentation.Requests.User;
using RealTimeWebChat.Presentation.Responses.User;

namespace RealTimeWebChat.Application.Services.UserServices
{
    public class UserService : IUserService
    {
        private readonly IUserRepository userRepository;
        private readonly IPasswordHasher<User> passwordHasher;
        public UserService(IUserRepository userRepository, IPasswordHasher<User> passwordHasher)
        {
            this.userRepository = userRepository;
            this.passwordHasher = passwordHasher;
        }
        public async Task<CreateUserResponse> CreateUserAsync(CreateUserRequest request)
        {
            var user = new User()
            {
                Name = request.Name
            };
            var passwordHash = passwordHasher.HashPassword(user, request.Password);
            user.PasswordHash = passwordHash;
            await userRepository.AddAsync(user);

            return new CreateUserResponse()
            {
                Id = user.Id,
                Name = user.Name
            };
        }

        public async Task SoftDeleteUserAsync(DeleteUserRequest request)
        {
            await userRepository.SoftDeleteAsync(request.Id);
        }

        public async Task<GetUserResponse> GetUserByNameAsync(GetUserRequest request)
        {
            var user = await userRepository.GetByNameAsync(request.UserName);
            if (user == null)
                throw new Exception("User not found");
            return new GetUserResponse()
            {
                Id = user.Id,
                Name = user.Name
            };
        }

        public async Task<UpdateUserResponse> UpdateUserAsync(UpdateUserRequest request)
        {
            var user = await userRepository.GetByIdAsync(request.Id);
            if (!string.IsNullOrWhiteSpace(request.Name))
                user.Name = request.Name;
            if (!string.IsNullOrWhiteSpace(request.NewPassword))
            {
                var verify = passwordHasher.VerifyHashedPassword(
                    user,
                    user.PasswordHash,
                    request.OldPassword
                );

                if (verify == PasswordVerificationResult.Success)
                    user.PasswordHash = passwordHasher.HashPassword(user, request.NewPassword);
            }
            await userRepository.SaveChangesAsync();

            return new UpdateUserResponse()
            {
                Id = user.Id,
                Name = user.Name,
            };
        }
    }
}
