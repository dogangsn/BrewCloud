using MassTransit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VetSystems.Shared.Events;

namespace VetSystems.Account.Application.Features.Consumers
{
    public class CreateAccountConsumer : IConsumer<CreateSubscriptionRequestEvent>
    {
        public Task Consume(ConsumeContext<CreateSubscriptionRequestEvent> context)
        {
            throw new NotImplementedException();
        }
    }
}
