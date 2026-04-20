using Microsoft.AspNetCore.Mvc;
using RealTimeWebChat.Application.Services.UserLayer;
using RealTimeWebChat.Presentation.Requests;
using RealTimeWebChat.Presentation.Requests.User;

namespace RealTimeWebChat.Presentation.Controllers
{
    [ApiController]
    [Route("api/users")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
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
    }
}