using Microsoft.AspNetCore.Identity;
using RealTimeWebChat.Helpers;
using RealTimeWebChat.Presentation.Requests;
using RealTimeWebChat.Presentation.Requests.User;
using RealTimeWebChat.Presentation.Response.User;
using RealTimeWebChat.Presentation.Responses.User;

namespace RealTimeWebChat.Application.Services.UserServices
{
    public class UserService : IUserService
    {
        private readonly IUserRepository userRepository;
        private readonly IPasswordHasher<User> passwordHasher;
        private readonly IWebHostEnvironment _environment;
        public UserService(IUserRepository userRepository, IPasswordHasher<User> passwordHasher, IWebHostEnvironment environment)
        {
            this.userRepository = userRepository;
            this.passwordHasher = passwordHasher;
            _environment = environment;
        }
        public async Task<CreateUserResponse> CreateUserAsync(CreateUserRequest request)
        {
            bool isUsernameAlreadyTaken = await userRepository.GetByNameAsync(request.Name) != null;
            if (isUsernameAlreadyTaken)
                throw new Exception("Username is already taken");
            
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

        public async Task<UserDto> GetUserById(int userId)
        {
            var user = await userRepository.GetByIdAsync(userId);
            if (user == null)
                throw new Exception("User not found");
            return new UserDto
            {
                AvatarUrl = user.AvatarUrl,
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

        public async Task<AvatarUploadEvent> UploadAvatar(IFormFile avatar, int userId)
        {
            if (avatar == null || avatar.Length == 0)
                throw new ArgumentException("Файл пуст");
            if(!AvatarTypeHelper.IsAllowedMimeType(avatar.ContentType))
                throw new ArgumentException("Недопустимый MIME тип");

            var fileExtension = Path.GetExtension(avatar.FileName).ToLowerInvariant();
            if (!AvatarTypeHelper.IsAllowedExtensions(fileExtension))
                throw new ArgumentException("Недопустимый расширение файла");

            var uploadsPath = Path.Combine(
                _environment.WebRootPath,
                "uploads",
                "avatars");

            Directory.CreateDirectory(uploadsPath);

            var fileName = $"{Guid.NewGuid()}{fileExtension}";

            var filePath = Path.Combine(uploadsPath, fileName);

            using var stream = new FileStream(filePath, FileMode.Create);

            await avatar.CopyToAsync(stream);

            var avatarUrl = $"/uploads/avatars/{fileName}";

            var user = await userRepository.GetByIdAsync(userId);
            if (user == null)
                throw new Exception("User not found");
            user.AvatarUrl = avatarUrl;

            await userRepository.SaveChangesAsync();

            return new AvatarUploadEvent
            {
                AvatarUrl = avatarUrl,
                UserId = userId
            };
        }
    }
}
