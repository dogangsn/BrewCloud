using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VetSystems.Shared.Events;
using VetSystems.Vet.Application.Services.Mails;

namespace VetSystems.Vet.Application.Event
{
    public class SendMailEvent : INotification
    {
        public string EmailToId { get; set; }
        public string EmailToName { get; set; }
        public string EmailSubject { get; set; }
        public string EmailBody { get; set; }
    }

    public class SendMailEventHandler : INotificationHandler<SendMailEvent>
    {
        private readonly ILogger<SendMailEventHandler> _logger;
        private readonly IMediator _mediator;
        private readonly IMailService _mailService;

        public SendMailEventHandler(IMailService mailService , ILogger<SendMailEventHandler> logger, IMediator mediator)
        {
            _mailService = mailService;
            _logger = logger;
            _mediator = mediator;
        }

        public async Task Handle(SendMailEvent notification, CancellationToken cancellationToken)
        {
            var eventMessage = new SendMailRequestEvent()
            {
                EmailToId = notification.EmailToId,
                EmailToName = notification.EmailToName,
                EmailSubject = notification.EmailSubject,
                EmailBody = notification.EmailBody,
            };
            var resultdata = _mailService.SendMail($"mail/mailing/SendMail", eventMessage);
        }
    }
}
