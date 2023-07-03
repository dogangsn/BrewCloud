using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using VetSystems.IdentityServer.Infrastructure.Entities;
using VetSystems.IdentityServer.Infrastructure.Services.Interface;
using VetSystems.Shared.Service;
using VetSystems.Shared.Dtos;
using VetSystems.IdentityServer.Infrastructure.Repositories;
using Microsoft.Extensions.Configuration;

namespace VetSystems.IdentityServer.Application.Features.Accounts.Commands
{
    public class CreateTempCommand : IRequest<Response<bool>>
    {
        public string Email { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Company { get; set; }
        public string Phone { get; set; }
        public string Host { get; set; }
        public string ActivationCode { get; set; }
    }
    public class CreateTempCommandHandler : IRequestHandler<CreateTempCommand, Response<bool>>
    {
        private readonly IUnitOfWork _uow;
        private readonly ILogger<CreateTempCommandHandler> _logger;
        private readonly IAccountService _accountService;
        private readonly IConfiguration _configuration;
        private readonly IIdentityRepository _identity;
        private readonly IRepository<SubscriptionAccount> _tempRepository;


        public CreateTempCommandHandler(IUnitOfWork uow, IRepository<SubscriptionAccount> tempRepository, ILogger<CreateTempCommandHandler> logger, IAccountService accountService, IConfiguration configuration, IIdentityRepository identity)
        {
            _uow = uow ?? throw new ArgumentNullException(nameof(uow));
            _tempRepository = tempRepository ?? throw new ArgumentNullException(nameof(tempRepository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _accountService = accountService ?? throw new ArgumentNullException(nameof(accountService));
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
            _identity = identity;
        }

        public async Task<Response<bool>> Handle(CreateTempCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var user = await _accountService.GetAccountByEmail(request.Username.Trim());
                if (user != null)
                {
                    return Response<bool>.Fail("e-mail address is already taken ", 404);
                }
            }
            catch (Exception ex)
            {
            }

            var entity = await _tempRepository.FirstOrDefaultAsync(r => r.Username == request.Username && r.IsComplate == false);
            request.ActivationCode = GenerateRandomAlphanumericString();
            if (entity == null)
            {
                entity = new SubscriptionAccount
                {
                    Company = request.Company,
                    Password = request.Password,
                    Phone = request.Phone,
                    Username = request.Username,
                    Recid = Guid.NewGuid(),
                    ActivationCode = request.ActivationCode,
                    CreateDate = DateTime.Now,
                    Host = request.Host,
                    EMail = request.Email
                };
                var enterPriceResult = await _tempRepository.AddAsync(entity);
            }

            var enterPriseResult = await _uow.SaveChangesAsync(cancellationToken);
            return Response<bool>.Success(200); ;

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
