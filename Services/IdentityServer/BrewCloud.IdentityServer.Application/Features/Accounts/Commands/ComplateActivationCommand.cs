using MassTransit;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using BrewCloud.IdentityServer.Application.Services;
using BrewCloud.IdentityServer.Infrastructure.Entities;
using BrewCloud.Shared.Contracts;
using BrewCloud.Shared.Dtos;
using BrewCloud.Shared.Service;
using BrewCloud.IdentityServer.Infrastructure.Repositories;
using Microsoft.Extensions.Configuration;
using BrewCloud.IdentityServer.Application.Events;
using BrewCloud.Shared.Events;
using Microsoft.Identity.Client;

namespace BrewCloud.IdentityServer.Application.Features.Accounts.Commands
{
    public class ComplateActivationCommand : IRequest<Shared.Dtos.Response<bool>>
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

    public class ComplateActivationCommandHandler : IRequestHandler<ComplateActivationCommand, Shared.Dtos.Response<bool>>
    {
        private readonly IUnitOfWork _uow;
        private readonly IRepository<SubscriptionAccount> _tempRepository;
        private readonly IConfiguration _configuration;
        private readonly IIdentityRepository _identity;
        private readonly IAccountDataService _accountService;
        private readonly MediatR.IMediator _mediator;
        private readonly ISendEndpointProvider _sendEndpointProvider;
        private readonly ILogger<ComplateActivationCommandHandler> _logger;

        public ComplateActivationCommandHandler(IUnitOfWork uow, IRepository<SubscriptionAccount> tempRepository, 
            IConfiguration configuration, IIdentityRepository identity, IAccountDataService accountService, MediatR.IMediator mediator, ISendEndpointProvider sendEndpointProvider, ILogger<ComplateActivationCommandHandler> logger)
        {
            _uow = uow ?? throw new ArgumentNullException(nameof(uow));
            _tempRepository = tempRepository ?? throw new ArgumentNullException(nameof(tempRepository));
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
            _identity = identity;
            _accountService = accountService;
            _mediator = mediator;
            _sendEndpointProvider = sendEndpointProvider;
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<Shared.Dtos.Response<bool>> Handle(ComplateActivationCommand request, CancellationToken cancellationToken)
        {
            var temp = await _tempRepository.FirstOrDefaultAsync(r => r.EMail == request.Email && r.ActivationCode == request.ActivationCode  && r.IsComplate == false);
            if (temp == null)
            {
                return Shared.Dtos.Response<bool>.Fail("Activation Code is wrong. Please retry again", 404);
            }

            string serverName = _configuration["VetDbSettings:ServerName"];
            string user = _configuration["VetDbSettings:User"];
            string password = _configuration["VetDbSettings:Password"];
            string dbName = $"vet-{temp.Recid}";
            string connection = $"Server={serverName};Database={dbName};User Id={user};Password={password};";
            temp.ConnectionString = connection;

            try
            {
                //var hubEvent = new HubNotificationEvent
                //{
                //    Description = "Müşteri Database oluşturuluyor",
                //    RevenuCenterId = temp.Recid.ToString(),
                //};
                //var endpointHub = await _sendEndpointProvider.GetSendEndpoint(new Uri($"queue:{EventBusConstants.HubCreateAccountQueue}"));
                //await endpointHub.Send(hubEvent);

                var eventMessage = new CreateSubscriptionEvent
                {
                    RecId = temp.Recid,
                    Username = temp.Username,
                    Phone = temp.Phone,
                    Company = temp.Company,
                    ActivationCode = temp.ActivationCode,
                    Email = temp.EMail,
                    ConnectionString = connection,
                    Password = temp.Password,
                    TenantId = temp.Recid,
                    IsFirstCreate = false
                };

                var databaseResult = await CreateDatabase(eventMessage);

                if (databaseResult.ResponseType == ResponseType.Ok)
                {
                    await _mediator.Publish(eventMessage);
                    //await CreateModule(eventMessage.RecId);
                }
                temp.IsComplate = true;
                _tempRepository.Update(temp);
                await _uow.SaveChangesAsync(cancellationToken);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Exception: {ex.Message}");
            }
            return Shared.Dtos.Response<bool>.Success(200);

        }

        //private async Task<Shared.Dtos.Response<string>> CreateModule(Guid tenantId)
        //{
        //    var moduleRequest = new UpdateSubscriptionModuleCommand
        //    {
        //        TenantId = tenantId,
        //        Modules = new List<Models.SubscriptionModuleDto>
        //            {
        //                new Models.SubscriptionModuleDto
        //                {
        //                    ModuleId="pos",
        //                    StartDate = DateTime.Today.Date,
        //                    EndDate = DateTime.Today.AddDays(14).Date,
        //                    Selected=true,
        //                },
        //                new Models.SubscriptionModuleDto
        //                {
        //                    ModuleId="humanresources",
        //                    StartDate = DateTime.Today.Date,
        //                    EndDate = DateTime.Today.AddDays(14).Date,
        //                    Selected=true,
        //                }
        //            }
        //    };
        //    var result = await _mediator.Send(moduleRequest);
        //    return result;
        //}

        private async Task<Shared.Dtos.Response<bool>> CreateDatabase(CreateSubscriptionEvent eventMessage)
        {
            var databaseRequest = new CreateSubscriptionRequestEvent
            {
                RecId = eventMessage.RecId,
                Username = eventMessage.Username,
                Phone = eventMessage.Phone,
                Password = eventMessage.Password,
                Company = eventMessage.Company,
                ActivationCode = eventMessage.ActivationCode,
                Email = eventMessage.Email,
                ConnectionString = eventMessage.ConnectionString,
            };

            var resultDb = await _accountService.CreateDatabse(databaseRequest);
            if (resultDb.ResponseType != ResponseType.Ok)
            {
            }
            return resultDb;
        }



    }


}
