using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RealTimeWebChat.Application.Services.Participant;
using System.Security.Claims;

namespace RealTimeWebChat.Presentation.Controllers
{
    [ApiController]
    [Route("api/participants")]
    [Authorize]
    public class ParticipantController : ControllerBase
    {
        private readonly IParticipantService participantService;

        public ParticipantController(IParticipantService participantService)
        {
            this.participantService = participantService;
        }

        private int GetUserId()
        {
            return int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
        }

        [HttpPost("{chatId}/join")]
        public async Task<IActionResult> JoinChat(int chatId)
        {
            await participantService.JoinChatAsync(GetUserId(), chatId);
            return Ok();
        }

        [HttpDelete("{chatId}/leave")]
        public async Task<IActionResult> LeaveChat(int chatId)
        {
            await participantService.LeaveChatAsync(GetUserId(), chatId);
            return Ok();
        }
    }
}