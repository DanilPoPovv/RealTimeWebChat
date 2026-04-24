using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using RealTimeWebChat.Application.Services.Participant;
using RealTimeWebChat.Infrastructure.SignalR;
using System.Security.Claims;

[ApiController]
[Route("api/participants")]
[Authorize]
public class ParticipantController : ControllerBase
{
    private readonly IParticipantService participantService;
    private readonly IHubContext<ChatHub> hubContext;

    public ParticipantController(
        IParticipantService participantService,
        IHubContext<ChatHub> hubContext)
    {
        this.participantService = participantService;
        this.hubContext = hubContext;
    }

    private int GetUserId()
        => int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);

    [HttpPost("{chatId}/join")]
    public async Task<IActionResult> JoinChat(int chatId)
    {
        var dto = await participantService.JoinChatAsync(GetUserId(), chatId);

        await hubContext.Clients.Group(chatId.ToString())
            .SendAsync("UserJoined", dto);

        return Ok(dto);
    }

    [HttpDelete("{chatId}/leave")]
    public async Task<IActionResult> LeaveChat(int chatId)
    {
        var dto = await participantService.LeaveChatAsync(GetUserId(), chatId);

        await hubContext.Clients.Group(chatId.ToString())
            .SendAsync("UserLeft", dto);

        return Ok(dto);
    }
}