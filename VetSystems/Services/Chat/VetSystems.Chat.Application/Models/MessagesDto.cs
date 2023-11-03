using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VetSystems.Chat.Application.Models
{
    public class MessagesDto
    {
        public Guid Id { get; set; }
        public Guid? ChatId { get; set; }
        public Guid? ContactId { get; set; }
        public string Value { get; set; }
        public string CreatedAt { get; set; }

    }
}
