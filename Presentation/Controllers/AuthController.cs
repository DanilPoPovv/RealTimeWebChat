using Microsoft.AspNetCore.Mvc;
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
