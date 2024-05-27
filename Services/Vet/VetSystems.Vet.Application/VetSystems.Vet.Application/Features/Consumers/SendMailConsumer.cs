using MassTransit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VetSystems.Shared.Events;

namespace VetSystems.Vet.Application.Features.Consumers
{
    public class SendMailConsumer : IConsumer<SendMailRequestEvent>
    {
        public Task Consume(ConsumeContext<SendMailRequestEvent> context)
        {
            throw new NotImplementedException();
        }
    }
}
