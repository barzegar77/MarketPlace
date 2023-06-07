using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Models.Chat
{
    public class ChatRoom
    {
        [Key]
        public Guid Id { get; set; }
        public string ConnectionId { get; set; }
    }
}
