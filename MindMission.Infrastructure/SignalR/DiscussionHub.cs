using Microsoft.AspNetCore.SignalR;
using MindMission.Application.DTOs;

namespace MindMission.Infrastructure.SignalR
{
    public class DiscussionHub : Hub
    {
        public async Task SendComment(DiscussionDto discussion)
        {
            await Clients.All.SendAsync("ReceiveComment", discussion);
        }
    }
}
