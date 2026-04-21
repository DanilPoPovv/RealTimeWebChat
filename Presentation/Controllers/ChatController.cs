using Microsoft.AspNetCore.Mvc;
using RealTimeWebChat.Application.Services.ChatServices;
using RealTimeWebChat.Presentation.Requests.Chat;
using RealTimeWebChat.Presentation.Response.Chat;

namespace RealTimeWebChat.Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ChatController : ControllerBase
    {
        private readonly IChatService chatService;

        public ChatController(IChatService chatService)
        {
            this.chatService = chatService;
        }

        [HttpPost]
        public async Task<ActionResult<CreateChatResponse>> CreateChat(
            [FromBody] CreateChatRequest request)
        {
            var result = await chatService.CreateChatAsync(request);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<GetChatResponse>> GetChat(
            [FromRoute] int id,
            [FromQuery] int? lastMessages)
        {
            var request = new GetChatRequest
            {
                Id = id,
                LastMessages = lastMessages
            };

            var result = await chatService.GetChatAsync(request);
            return Ok(result);
        }

        [HttpPut]
        public async Task<ActionResult<UpdateChatResponse>> UpdateChat(
            [FromBody] UpdateChatRequest request)
        {
            var result = await chatService.UpdateChatAsync(request);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteChat([FromRoute] int id)
        {
            var request = new DeleteChatRequest
            {
                Id = id
            };

            await chatService.DeleteChatAsync(request);
            return NoContent();
        }
    }
}