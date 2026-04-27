using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RealTimeWebChat.Application.Services.ChatServices;
using RealTimeWebChat.Presentation.Requests.Chat;
using RealTimeWebChat.Presentation.Response.Chat;
using System.Security.Claims;

namespace RealTimeWebChat.Presentation.Controllers
{
    [ApiController]
    [Route("api/chats")]
    [Authorize]
    public class ChatController : ControllerBase
    {
        private readonly IChatService chatService;

        public ChatController(IChatService chatService)
        {
            this.chatService = chatService;
        }

        private int GetUserId()
        {
            return int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
        }

        [HttpPost]
        public async Task<ActionResult<CreateChatResponse>> CreateChat([FromBody] CreateChatRequest request)
        {
            var result = await chatService.CreateChatAsync(GetUserId(), request);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<GetChatResponse>> GetChat(
            int id,
            [FromQuery] int? lastMessages)
        {
            var request = new GetChatRequest
            {
                Id = id,
                LastMessages = lastMessages
            };

            var result = await chatService.GetChatAsync(GetUserId(), request);
            return Ok(result);
        }

        [HttpPut]
        public async Task<ActionResult<UpdateChatResponse>> Update([FromBody] UpdateChatRequest request)
        {
            var result = await chatService.UpdateChatAsync(GetUserId(), request);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var request = new DeleteChatRequest
            {
                Id = id
            };

            await chatService.DeleteChatAsync(GetUserId(), request);
            return NoContent();
        }
        [HttpGet]
        public async Task<List<ChatDto>> GetAllUserChats()
        {
            return await chatService.GetAllUserChats(GetUserId());
        }
    }
}