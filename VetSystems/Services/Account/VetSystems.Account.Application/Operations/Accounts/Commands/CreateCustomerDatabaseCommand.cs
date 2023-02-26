using MediatR;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VetSystems.Account.Domain.Contracts;
using VetSystems.Shared.Accounts;
using VetSystems.Shared.Dtos;

namespace VetSystems.Account.Application.Operations.Accounts.Commands
{
    public class CreateCustomerDatabaseCommand : IRequest<Response<bool>>
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string CompanyName { get; set; }
        public string Phone { get; set; }
    }


    public class CreateCustomerDatabaseResponseHandler : IRequestHandler<CreateCustomerDatabaseCommand, Response<bool>>
    {
        private readonly IMediator _mediator;
        private readonly IUnitOfWork _uof;
        private readonly TenantDbSettings _tenantSettings;

        public CreateCustomerDatabaseResponseHandler(IMediator mediator, IUnitOfWork uof, IOptions<TenantDbSettings> tenantSettings)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            _uof = uof ?? throw new ArgumentNullException(nameof(uof));
            _tenantSettings = tenantSettings.Value;
        }

        public async Task<Response<bool>> Handle(CreateCustomerDatabaseCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var tenant = new Tenant
                {
                    Company = request.CompanyName,
                    Id = Guid.NewGuid(),
                    _tenant = _tenantSettings
                };
                await _uof.MigrateDatabase(tenant);
            }
            catch (Exception ex)
            {
                throw;
            }

            return Response<bool>.Success(200);  
        }
    }
}
