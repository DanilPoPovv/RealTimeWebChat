using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using RealTimeWebChat.Application.Services.UserServices;
using RealTimeWebChat.Infrastructure.SignalR;
using RealTimeWebChat.Presentation.Requests;
using RealTimeWebChat.Presentation.Requests.User;
using System.Security.Claims;

namespace RealTimeWebChat.Presentation.Controllers
{
    [ApiController]
    [Route("api/users")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IHubContext<ChatHub> hubContext;

       
        private int GetUserId()
        {
            return int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
        }
        public UserController(IUserService userService,
                              IHubContext<ChatHub> hubContext)
        {
            _userService = userService;
            this.hubContext = hubContext;
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] CreateUserRequest request)
        {
            var result = await _userService.CreateUserAsync(request);
            return Ok(result);
        }

        [HttpGet("{name}")]
        public async Task<IActionResult> GetByName(string name)
        {
            var result = await _userService.GetUserByNameAsync(
                new GetUserRequest { UserName = name });

            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _userService.SoftDeleteUserAsync(
                new DeleteUserRequest { Id = id });

            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UpdateUserRequest request)
        {
            var result = await _userService.UpdateUserAsync(request);
            return Ok(result);
        }
        [HttpPost("avatar")]
        public async Task<IActionResult> UploadAvatar(IFormFile avatar)
        {
            var result = await _userService.UploadAvatar(avatar, GetUserId());
            await hubContext.Clients.Group(result.UserId.ToString()).SendAsync("AvatarUploaded", result);
            return Ok(result);
        }

    }
}