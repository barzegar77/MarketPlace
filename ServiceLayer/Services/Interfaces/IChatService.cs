using ServiceLayer.ViewModels.ChatViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Services.Interfaces
{
    public interface IChatService
    {
        Task<Guid> CreateChatRoom(string connectionId);
        Task<Guid> GetChatRoomForConnection(string connectionId);


        Task SaveChatMessage(ChatMessageViewModel model);
        Task<List<ChatMessageViewModel>> GetChatMessages(Guid roomId);

        Task<List<Guid>> GetAllRooms();
    }
}
