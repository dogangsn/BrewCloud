using MassTransit;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using VetSystems.IdentityServer.Application.Events;
using VetSystems.IdentityServer.Infrastructure.Entities;
using VetSystems.Shared.Dtos;
using VetSystems.IdentityServer.Infrastructure.Repositories;

namespace VetSystems.IdentityServer.Application.Features.Accounts.Commands
{
    public class ComplateSubscriptionCommand : IRequest<Shared.Dtos.Response<bool>>
    {
        public string Email { get; set; }
        public string ActivationCode { get; set; }
        public string TimeZone { get; set; }
        public string EndOfTime { get; set; }
        public string Language { get; set; }
        public Guid TimeZoneOwnerId { get; set; }
        public Guid CorrelationId { get; set; }
        public string Host { get; set; }
    }
    public class ComplateSubscriptionCommandHandler : IRequestHandler<ComplateSubscriptionCommand, Shared.Dtos.Response<bool>>
    {
        private readonly ILogger<ComplateSubscriptionCommandHandler> _logger;
        private readonly IRepository<SubscriptionAccount> _tempRepository;
        private readonly ISendEndpointProvider _sendEndpointProvider;
        private readonly IMediator _mediator;
        public ComplateSubscriptionCommandHandler(ILogger<ComplateSubscriptionCommandHandler> logger, IRepository<SubscriptionAccount> tempRepository, ISendEndpointProvider sendEndpointProvider, IMediator mediator)
        {
            _logger = logger;
            _tempRepository = tempRepository;
            _sendEndpointProvider = sendEndpointProvider;
            _mediator = mediator;
        }

        public async Task<Shared.Dtos.Response<bool>> Handle(ComplateSubscriptionCommand request, CancellationToken cancellationToken)
        {
            var temp = await _tempRepository.FirstOrDefaultAsync(r => r.ActivationCode == request.ActivationCode && r.Username == request.Email && r.IsComplate == false);
            if (temp == null)
            {
                return Shared.Dtos.Response<bool>.Fail("Activation Code is wrong. Please retry again", 404);
            }
            var eventMessage = new CreateSubscriptionEvent
            {
                RecId = temp.Recid,
                Username = temp.Username,
                Phone = temp.Phone,
                Password = temp.Password,
                Company = temp.Company,
                ActivationCode = request.ActivationCode,
                Email = request.Email,
            };
            await _mediator.Send(eventMessage);

            return Shared.Dtos.Response<bool>.Success(200);
        }
    }
}
