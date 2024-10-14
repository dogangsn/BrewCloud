using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using BrewCloud.Account.Application.GrpServices;
using BrewCloud.Account.Domain.Contracts;
using BrewCloud.Account.Domain.Entities;
using BrewCloud.Shared.Dtos;
using BrewCloud.Shared.Service;

namespace BrewCloud.Account.Application.Features.Account.Commands
{
    public class CreateTempCommand : IRequest<Response<bool>>
    {
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
        //private readonly IAccountService _accountService;
        private readonly IdentityGrpService _identityGrpService;
        private readonly IConfiguration _configuration;
        private readonly IIdentityRepository _identity;
        private readonly IRepository<TempAccount> _tempRepository;


        public CreateTempCommandHandler(IUnitOfWork uow, IRepository<TempAccount> tempRepository, ILogger<CreateTempCommandHandler> logger, IdentityGrpService identityGrpService, IConfiguration configuration, IIdentityRepository identity)
        {
            _uow = uow ?? throw new ArgumentNullException(nameof(uow));
            _tempRepository = tempRepository ?? throw new ArgumentNullException(nameof(tempRepository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _identityGrpService = identityGrpService ?? throw new ArgumentNullException(nameof(identityGrpService));
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
            _identity = identity;
        }

        public async Task<Response<bool>> Handle(CreateTempCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var user = await _identityGrpService.GetUserByEmailAsync(request.Username.Trim());
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
                entity = new TempAccount
                {
                    Company = request.Company,
                    Password = request.Password,
                    Phone = request.Phone,
                    Username = request.Username,
                    Id = Guid.NewGuid(),
                    ActivationCode = request.ActivationCode,
                    CreateDate = DateTime.Now,
                    //Host = request.Host,
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
