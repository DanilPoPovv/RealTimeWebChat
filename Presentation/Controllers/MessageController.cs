using Microsoft.AspNetCore.Mvc;
using RealTimeWebChat.Application.Services.MessageService;
using RealTimeWebChat.Presentation.Requests.Message;
using RealTimeWebChat.Presentation.Response.Message;

namespace RealTimeWebChat.Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MessageController : ControllerBase
    {
        private readonly IMessageService messageService;

        public MessageController(IMessageService messageService)
        {
            this.messageService = messageService;
        }

        [HttpPost]
        public async Task<ActionResult<MessageDto>> SendMessage([FromBody] SendMessageRequest request)
        {
            var messageDto = await messageService.SendMessageAsync(request);
            return Ok(messageDto);
        }

       
        [HttpGet("chat/{chatId}")]
        public async Task<ActionResult<List<MessageDto>>> GetMessages(
            [FromRoute] int chatId,
            [FromQuery] int messageCount,
            [FromQuery] int pageCount = 0)
        {
            var messages = await messageService
                .GetLastChatMessagesAsync(chatId, messageCount, pageCount);

            return Ok(messages);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateMessage([FromBody] UpdateMessageRequest request)
        {
            await messageService.UpdateMessageAsync(request);
            return NoContent();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteMessage([FromBody] DeleteMessageRequest request)
        {
            await messageService.DeleteMessageAsync(request);
            return NoContent();
        }
    }
}