using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RealTimeWebChat.Application.Services.MessageService;
using RealTimeWebChat.Presentation.Requests.Message;
using RealTimeWebChat.Presentation.Response.Message;
using System.Security.Claims;

namespace RealTimeWebChat.Presentation.Controllers
{
    [ApiController]
    [Route("api/messages")]
    [Authorize]
    public class MessageController : ControllerBase
    {
        private readonly IMessageService messageService;

        public MessageController(IMessageService messageService)
        {
            this.messageService = messageService;
        }

        private int GetUserId()
        {
            return int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
        }

        [HttpPost]
        public async Task<ActionResult<MessageDto>> SendMessage([FromBody] SendMessageRequest request)
        {
            var result = await messageService.SendMessageAsync(GetUserId(), request);
            return Ok(result);
        }

        [HttpGet("chat/{chatId}")]
        public async Task<ActionResult<List<MessageDto>>> GetMessages(
            int chatId,
            [FromQuery] int messageCount,
            [FromQuery] int pageCount = 0)
        {
            var result = await messageService
                .GetLastChatMessagesAsync(GetUserId(), chatId, messageCount, pageCount);

            return Ok(result);
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UpdateMessageRequest request)
        {
            await messageService.UpdateMessageAsync(GetUserId(), request);
            return NoContent();
        }

        [HttpDelete]
        public async Task<IActionResult> Delete([FromBody] DeleteMessageRequest request)
        {
            await messageService.DeleteMessageAsync(GetUserId(), request);
            return NoContent();
        }
    }
}