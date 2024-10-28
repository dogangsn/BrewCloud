using BrewCloud.Account.Domain.Contracts;
using BrewCloud.Account.Domain.Entities;
using BrewCloud.Shared.Dtos;
using BrewCloud.Shared.Service;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace BrewCloud.Account.Application.Features.Settings.Commands
{
    public class CreateBranchCommand : IRequest<Response<string>>
    {
        public string Name { get; set; }
        public string District { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
    }

    public class CreateBranchHandler : IRequestHandler<CreateBranchCommand, Response<string>>
    {
        private readonly IUnitOfWork _uow;
        private readonly IRepository<Branch> _branchRepository;
        private readonly IIdentityRepository _identity;
        private readonly ILogger<CreateBranchHandler> _logger;

        public CreateBranchHandler(IUnitOfWork uow, IIdentityRepository identity, ILogger<CreateBranchHandler> logger, IRepository<Branch> branchRepository)
        {
            _identity = identity ?? throw new ArgumentNullException(nameof(identity));
            _uow = uow ?? throw new ArgumentNullException(nameof(uow));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _branchRepository = branchRepository ?? throw new ArgumentNullException(nameof(branchRepository));
        }

        public async Task<Response<string>> Handle(CreateBranchCommand request, CancellationToken cancellationToken)
        {
            var response = new Response<string>
            {
                ResponseType = ResponseType.Ok,
                IsSuccessful = true
            };
            var enterpriseId = _identity.Account.EnterpriseId;
            var userId = _identity.Account.UserId;

            try
            {
                var branch = new Branch
                {
                    Name = request.Name,
                    District = request.District,
                    Address = request.Address,
                    Phone = request.Phone,
                    CompanyId = enterpriseId,

                    CreateDate = DateTime.Now,
                    CreateUser = userId.ToString(),
                };
                await _branchRepository.AddAsync(branch);
                await _uow.SaveChangesAsync(cancellationToken);
            }
            catch (Exception ex)
            {
                response.IsSuccessful = false;
                response.ResponseType = ResponseType.Error;
                Response<string>.Fail(ex.Message, 400);
                _logger.LogInformation("Branch succesfully saved");
            }
            return response;
        }
    }
}
