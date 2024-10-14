using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BrewCloud.Shared.Service;

namespace BrewCloud.Vet.Infrastructure.Event
{
    public class CreateLogEventHandler : INotificationHandler<CreateLogEvent>
    {
        private readonly ILogService _logService;
        public CreateLogEventHandler(ILogService logService)
        {
            _logService = logService;
        }

        public Task Handle(CreateLogEvent notification, CancellationToken cancellationToken)
        {
            _logService.CreateLog(notification.Logs);
            return Task.CompletedTask;  
        }
    }
}
