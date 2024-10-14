using System;
using System.Collections.Generic;
using System.Text;

namespace BrewCloud.Shared.Contracts
{
    public interface ISendMessageRequest
    {
        public string UserName { get; set; }
        public string PassWord { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
    }
}
