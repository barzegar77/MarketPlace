using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Models.Chat
{
    public class ChatMessage
    {
        [Key]
        public Guid Id { get; set; }

        public string Sender { get; set; }
        public string Message { get; set; }
        public DateTime CreateDate { get; set; }

        public Guid ChatRoomId { get; set; }
        [ForeignKey(nameof(ChatRoomId))]
        public ChatRoom ChatRoom { get; set; }
    }
}
