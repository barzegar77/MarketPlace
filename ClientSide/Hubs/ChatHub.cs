using Microsoft.AspNetCore.SignalR;

namespace ClientSide.Hubs
{
    public class ChatHub : Hub
    {
        private static int counter = 0;
        public override async Task OnConnectedAsync()
        {
            counter++;
            await Clients.All.SendAsync("updateOnlineUserCount", counter);
           await base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            counter--;
            await Clients.All.SendAsync("updateOnlineUserCount", counter);
            await base.OnDisconnectedAsync(exception);
        }
    }
}
