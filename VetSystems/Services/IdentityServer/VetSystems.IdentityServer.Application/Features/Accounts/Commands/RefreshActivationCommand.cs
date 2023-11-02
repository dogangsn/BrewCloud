using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using VetSystems.IdentityServer.Infrastructure.Entities;
using VetSystems.IdentityServer.Infrastructure.Repositories;
using VetSystems.IdentityServer.Infrastructure.Services.Interface;
using VetSystems.Shared.Dtos;
using VetSystems.Shared.Service;

namespace VetSystems.IdentityServer.Application.Features.Accounts.Commands
{
    public class RefreshActivationCommand : IRequest<Response<bool>>
    {
        public string Username { get; set; }
        public string Phone { get; set; }
    }

    public class RefreshActivationCommandHandler : IRequestHandler<RefreshActivationCommand, Response<bool>>
    {
        private readonly IUnitOfWork _uow;
        private readonly ILogger<CreateTempCommandHandler> _logger;
        private readonly IAccountService _accountService;
        private readonly IConfiguration _configuration;
        private readonly IIdentityRepository _identity;
        private readonly IRepository<SubscriptionAccount> _tempRepository;

        public RefreshActivationCommandHandler(IUnitOfWork uow, IRepository<SubscriptionAccount> tempRepository, ILogger<CreateTempCommandHandler> logger, IAccountService accountService, IConfiguration configuration, IIdentityRepository identity)
        {
            _uow = uow ?? throw new ArgumentNullException(nameof(uow));
            _tempRepository = tempRepository ?? throw new ArgumentNullException(nameof(tempRepository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _accountService = accountService ?? throw new ArgumentNullException(nameof(accountService));
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
            _identity = identity;
        }

        public async Task<Response<bool>> Handle(RefreshActivationCommand request, CancellationToken cancellationToken)
        {

            var entity = await _tempRepository.FirstOrDefaultAsync(r => r.EMail == request.Username && r.IsComplate == false);
            if (entity != null)
            {
                entity.UpdateDate = DateTime.UtcNow;
                entity.ActivationCode = GenerateRandomAlphanumericString();
            }
            var enterPriseResult = await _uow.SaveChangesAsync(cancellationToken);

            return Response<bool>.Success(200); 
        }

        public string GenerateRandomAlphanumericString(int length = 10)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            var random = new Random();
            var randomString = new string(Enumerable.Repeat(chars, length)
                                                    .Select(s => s[random.Next(s.Length)]).ToArray());
            return randomString;
        }

    }
}
