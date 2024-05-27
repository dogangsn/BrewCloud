using MediatR;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using VetSystems.Account.Domain.Contracts;
using VetSystems.Shared.Accounts;
using VetSystems.Shared.Dtos;

namespace VetSystems.Account.Application.Features.Account.Commands
{
    public class CreateSubscriptionCommand : IRequest<Response<bool>>
    {
        public Guid TempId { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Company { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string ActivationCode { get; set; }
        public string ConnectionString { get; set; }
    }

    public class CreateSubscriptionCommandHandler : IRequestHandler<CreateSubscriptionCommand, Response<bool>>
    {
        private readonly IMediator _mediator;
        private readonly ILogger<CreateSubscriptionCommandHandler> _logger;
        private readonly Domain.Contracts.IUnitOfWork _uof;
        private readonly Microsoft.Extensions.Hosting.IHost _host;

        public CreateSubscriptionCommandHandler(IMediator mediator, ILogger<CreateSubscriptionCommandHandler> logger, IUnitOfWork uof, IHost host)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _uof = uof ?? throw new ArgumentNullException(nameof(uof));
            _host = host ?? throw new ArgumentNullException(nameof(host));
        }

        public async Task<Response<bool>> Handle(CreateSubscriptionCommand request, CancellationToken cancellationToken)
        {
            var response = Response<bool>.Success(200);
            try
            {
                var tenant = new Tenant
                {
                    Name = request.Company,
                    Id = Guid.NewGuid(),
                    DatabaseConnectionString = request.ConnectionString
                };
                await _uof.MigrateDatabase(tenant.DatabaseConnectionString);

                response.Data = true;
                response.IsSuccessful = true;
                response.ResponseType = ResponseType.Ok;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Database errors: {request.ConnectionString} - {(ex.InnerException != null ? ex.InnerException.Message : ex.Message)}");
                response.ResponseType = ResponseType.Error;
                response.Data = false;
                response.IsSuccessful = false;
                //response.Message = $"{request.ConnectionString} - {(ex.InnerException != null ? ex.InnerException.Message : ex.Message)}";
            }
            return response;
        }
    }
}
