using Microsoft.AspNetCore.Identity;
using RealTimeWebChat.Application.Services.UserServices;
using RealTimeWebChat.Presentation.Requests.Login;

namespace RealTimeWebChat.Application.Services.AuthServices
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository userRepository;
        private readonly IJwtService jwtService;
        private readonly IPasswordHasher<User> passwordHasher;

        public AuthService(
            IUserRepository userRepository,
            IJwtService jwtService,
            IPasswordHasher<User> passwordHasher)
        {
            this.userRepository = userRepository;
            this.jwtService = jwtService;
            this.passwordHasher = passwordHasher;
        }

        public async Task<string> LoginAsync(LoginRequest request)
        {
            var user = await userRepository.GetByNameAsync(request.Login);

            if (user == null)
                throw new Exception("User not found");

            var result = passwordHasher.VerifyHashedPassword(
                user,
                user.PasswordHash,
                request.Password);

            if (result == PasswordVerificationResult.Failed)
                throw new Exception("Invalid password");

            return jwtService.GenerateToken(user);
        }
    }
}
