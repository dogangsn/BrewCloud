using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BrewCloud.Vet.Domain.Entities;

namespace BrewCloud.Vet.Application.Models.Definition.SmsTemplate
{
    public class SmsTemplateListDto
    {
        public Guid Id { get; set; }
        public bool Active { get; set; }
        public string TemplateName { get; set; } = string.Empty;
        public string TemplateContent { get; set; } = string.Empty;
        public bool? EnableSMS { get; set; }
        public bool? EnableAppNotification { get; set; }
        public bool? EnableEmail { get; set; }
        public bool? EnableWhatsapp { get; set; }
        public SmsType SmsType { get; set; }
    }
}
