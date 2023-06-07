using Microsoft.AspNetCore.SignalR;
using ServiceLayer.Services.Interfaces;

namespace ClientSide.Hubs
{
    public class SupportHub : Hub
    {
        private readonly IChatService _chatService;

        public SupportHub(IChatService chatService)
        {
            _chatService = chatService;
        }

        public async override Task OnConnectedAsync()
        {
            var rooms = await _chatService.GetAllRooms();
            await Clients.Caller.SendAsync("GetRooms", rooms);
            await base.OnConnectedAsync();
        }
    }
}
