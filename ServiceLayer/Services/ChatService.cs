using DataLayer.Context;
using DataLayer.Models.Chat;
using Microsoft.EntityFrameworkCore;
using ServiceLayer.Services.Interfaces;
using ServiceLayer.ViewModels.ChatViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Services
{
    public class ChatService : IChatService
    {
        private readonly ApplicationDbContext _context;

        public ChatService(ApplicationDbContext context)
        {
            _context = context;
        }


        public async Task<Guid> CreateChatRoom(string connectionId)
        {
            var existChatRoom =  _context.ChatRooms.SingleOrDefault(x => x.ConnectionId == connectionId);
            if(existChatRoom != null)
            {
                return await Task.FromResult(existChatRoom.Id);
            }

            ChatRoom chatRoom = new ChatRoom
            {
                ConnectionId = connectionId,
                Id = Guid.NewGuid(),
            };

            _context.ChatRooms.Add(chatRoom);
            _context.SaveChanges();
            return await Task.FromResult(chatRoom.Id);
        }


        public async Task<Guid> GetChatRoomForConnection(string connectionId)
        {
            var chatRoom =  _context.ChatRooms.SingleOrDefault(x => x.ConnectionId == connectionId);
            return await Task.FromResult(chatRoom.Id);
        }



        public async Task SaveChatMessage(ChatMessageViewModel model)
        {
            ChatMessage chatMessage = new ChatMessage
            {
                ChatRoomId = model.ChatRoomId,
                CreateDate = DateTime.Now,
                Message = model.Message,
                Sender = model.Sender,
                Id = Guid.NewGuid(),
            };

            await _context.ChatMessages.AddAsync(chatMessage);
            await _context.SaveChangesAsync();
        }


        public async Task<List<ChatMessageViewModel>> GetChatMessages(Guid roomId)
        {
            return await _context.ChatMessages.Where(x => x.ChatRoomId == roomId)
                .Select(x => new ChatMessageViewModel
                {
                    Message = x.Message,
                    Sender = x.Sender,
                }).ToListAsync();
        }



        public async Task<List<Guid>> GetAllRooms()
        {
            return await _context.ChatRooms.Select(x => x.Id).ToListAsync();
        }
    }
}
