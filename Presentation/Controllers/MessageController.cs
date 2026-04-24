using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using RealTimeWebChat.Application.Services.MessageService;
using RealTimeWebChat.Infrastructure.SignalR;
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
        private readonly IHubContext<ChatHub> hubContext;
        public MessageController(IMessageService messageService,
                                 IHubContext<ChatHub> hubContext)
        {
            this.messageService = messageService;
            this.hubContext = hubContext;
        }

        private int GetUserId()
        {
            return int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
        }

        [HttpPost]
        public async Task<ActionResult<MessageReceivedEventDto>> SendMessage([FromBody] SendMessageRequest request)
        {
            var result = await messageService.SendMessageAsync(GetUserId(), request);
            await hubContext.Clients.Group(result.ChatId.ToString()).
                                SendAsync("ReceiveMessage", result);
            return Ok(result);
        }

        [HttpGet("chat/{chatId}")]
        public async Task<ActionResult<List<MessageReceivedEventDto>>> GetMessages(
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
            var updateDto = await messageService.UpdateMessageAsync(GetUserId(), request);
            await hubContext.Clients.Group(updateDto.ChatId.ToString()).
                    SendAsync("UpdateMessage", updateDto);
            return NoContent();
        }

        [HttpDelete]
        public async Task<IActionResult> Delete([FromBody] DeleteMessageRequest request)
        {
            var deleteDto = await messageService.DeleteMessageAsync(GetUserId(), request);
            await hubContext.Clients.Group(deleteDto.ChatId.ToString()).
                                  SendAsync("DeleteMessage", deleteDto);
            return NoContent();
        }
    }
}