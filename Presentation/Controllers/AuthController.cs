using Microsoft.AspNetCore.Mvc;
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

