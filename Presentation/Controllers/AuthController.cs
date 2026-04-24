using Microsoft.AspNetCore.Mvc;
<<<<<<< HEAD
using RealTimeWebChat.Application.Services.JwtService;
using RealTimeWebChat.Application.Services.UserServices;

namespace RealTimeWebChat.Presentation.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly IJwtService _jwtService;

        public AuthController(IUserRepository userRepository, IJwtService jwtService)
        {
            _userRepository = userRepository;
            _jwtService = jwtService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(string name, string password)
        {
            var user = await _userRepository.GetByNameAsync(name);

            if (user == null)
                return Unauthorized();

            var token = _jwtService.GenerateToken(user);

            return Ok(new { token });
        }
    }
}
=======
using RealTimeWebChat.Application.Services.AuthServices;
using RealTimeWebChat.Presentation.Requests.Login;

[ApiController]
[Route("api/auth")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequest request)
    {
        var token = await _authService.LoginAsync(request);

        return Ok(new { token });
    }
}
>>>>>>> cb2d70e82eb25df5a788c33b1f2258da0e0721d6
