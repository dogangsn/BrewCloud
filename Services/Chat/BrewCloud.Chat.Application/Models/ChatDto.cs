using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrewCloud.Chat.Application.Models
{
    public class ChatDto
    {
        public ChatDto()
        {
            Contact = new ContactDto();
            Messages = new List<MessagesDto>();
        }

        public Guid? Id { get; set; }
        public Guid? ContactId { get; set; }
        public ContactDto Contact { get; set; }
        public int UnreadCount { get; set; }
        public bool Muted { get; set; }
        public string LastMessage { get; set; } = string.Empty;
        public string lLstMessageAt { get; set; } = string.Empty;
        public List<MessagesDto> Messages { get; set; }
    }
}
