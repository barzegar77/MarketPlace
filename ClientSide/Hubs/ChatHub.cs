using Microsoft.AspNetCore.SignalR;
using ServiceLayer.Services.Interfaces;

namespace ClientSide.Hubs
{
    public class ChatHub : Hub
    {
        private static int counter = 0;
        private readonly IChatService _chatService;
        public ChatHub(IChatService chatService)
        {
            _chatService = chatService;
        }

     
        public async Task SendNewMessage(string sender, string message)
        {
            if(message == "/createNewChat")
            {
                var roomId = await _chatService.CreateChatRoom(Context.ConnectionId);

                await Groups.AddToGroupAsync(Context.ConnectionId, roomId.ToString());
                await Clients.Caller.SendAsync("getNewMessage", "پشتیبانی", "سلام به سایت ما خوش آمدید");
            }
            else
            {
                var roomId = await _chatService.GetChatRoomForConnection(Context.ConnectionId);
                await Clients.Groups(roomId.ToString()).SendAsync("getNewMessage", sender, message);
            }

        }



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
