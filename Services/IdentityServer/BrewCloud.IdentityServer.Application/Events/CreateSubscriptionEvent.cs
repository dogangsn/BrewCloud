using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using BrewCloud.IdentityServer.Application.Services;
using BrewCloud.Shared.Contracts;
using BrewCloud.Shared.Dtos;
using BrewCloud.Shared.Events;
using BrewCloud.IdentityServer.Application.Models;

namespace BrewCloud.IdentityServer.Application.Events
{
    public class CreateSubscriptionEvent : INotification
    {
        public Guid RecId { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Company { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string ActivationCode { get; set; }
        public string ConnectionString { get; set; }
        public Guid TenantId { get; set; }
        public bool IsFirstCreate { get; set; }
    }
    public class CreateSubscriptionEventHandler : INotificationHandler<CreateSubscriptionEvent>
    {
        private readonly IAccountDataService _accountService;
        private readonly ILogger<CreateSubscriptionEventHandler> _logger;
        private readonly IMediator _mediator;
        public CreateSubscriptionEventHandler(IAccountDataService accountService, ILogger<CreateSubscriptionEventHandler> logger, IMediator mediator)
        {
            _accountService = accountService;
            _logger = logger;
            _mediator = mediator;
        }

        public async Task Handle(CreateSubscriptionEvent temp, CancellationToken cancellationToken)
        {
            var eventMessage = new CreateSubscriptionRequestEvent
            {
                RecId = temp.RecId,
                Username = temp.Email,
                Phone = temp.Phone,
                Password = temp.Password,
                Company = temp.Company,
                ActivationCode = temp.ActivationCode,
                Email = temp.Email,
                ConnectionString = temp.ConnectionString,
                IsFirstCreate = false,
                FirstLastName = temp.Username
            };
            var resultData = await _accountService.SeedData(eventMessage);
            if (resultData.ResponseType != ResponseType.Ok)
            {
                _logger.LogError($"SeedData Err: {resultData.Errors.ToArray()}");
            }

            var dim = new string[] { "vet", "mail" };
            foreach (var item in dim)
            {
                var hrResultData = await _accountService.SendAsync($"{item}/account/UpdateDatabase", eventMessage);
                if (hrResultData.ResponseType != ResponseType.Ok)
                {
                    //isError = true;
                    _logger.LogError($"UpdateDatabase Err: {item}-{resultData.Errors.ToArray()}");
                }
            }
        }
    }

}
