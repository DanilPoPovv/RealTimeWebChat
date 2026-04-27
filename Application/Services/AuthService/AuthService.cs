using Microsoft.AspNetCore.Identity;
using RealTimeWebChat.Application.Services.AuthServices;
using RealTimeWebChat.Application.Services.UserServices;
using RealTimeWebChat.Presentation.Requests.Login;

public class AuthService : IAuthService
{
    private readonly IUserRepository userRepository;
    private readonly IPasswordHasher<User> passwordHasher;
    private readonly IJwtService jwtService;

    public AuthService(
        IUserRepository userRepository,
        IPasswordHasher<User> passwordHasher,
        IJwtService jwtService)
    {
        this.userRepository = userRepository;
        this.passwordHasher = passwordHasher;
        this.jwtService = jwtService;
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

        if (result != PasswordVerificationResult.Success)
            throw new Exception("Invalid password");

        return jwtService.GenerateToken(user);
    }
}