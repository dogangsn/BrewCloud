using System;
using System.Collections.Generic;
using System.Text;
using BrewCloud.Shared.Contracts;

namespace BrewCloud.Shared.Events
{
    public class SendMessageRequestEvent : IntegrationBaseEvent, ISendMessageRequest
    {
        public string UserName { get; set; }
        public string PassWord { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string[] SendPhone { get; set; }
    }
}
