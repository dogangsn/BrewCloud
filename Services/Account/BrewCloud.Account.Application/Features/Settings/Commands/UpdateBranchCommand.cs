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
    public class UpdateBranchCommand : IRequest<Response<string>>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string District { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
    }

    public class UpdateBranchHandler : IRequestHandler<UpdateBranchCommand, Response<string>>
    {
        private readonly IUnitOfWork _uow;
        private readonly IRepository<Branch> _branchRepository;
        private readonly IIdentityRepository _identity;
        private readonly ILogger<UpdateBranchHandler> _logger;

        public UpdateBranchHandler(IUnitOfWork uow, IIdentityRepository identity, ILogger<UpdateBranchHandler> logger, IRepository<Branch> branchRepository)
        {
            _identity = identity ?? throw new ArgumentNullException(nameof(identity));
            _uow = uow ?? throw new ArgumentNullException(nameof(uow));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _branchRepository = branchRepository ?? throw new ArgumentNullException(nameof(branchRepository));
        }

        public async Task<Response<string>> Handle(UpdateBranchCommand request, CancellationToken cancellationToken)
        {
            var response = new Response<string>
            {
                ResponseType = ResponseType.Ok,
                IsSuccessful = true
            };
            var userId = _identity.Account.UserId;

            try
            {
                Branch branch = _branchRepository.GetByIdAsync(request.Id).Result;
                if (branch != null)
                {
                    branch.Name = request.Name;
                    branch.District = request.District;
                    branch.Address = request.Address;
                    branch.Phone = request.Phone;
                    branch.UpdateDate = DateTime.Now;
                    branch.UpdateUser = userId.ToString();
                }

                await _uow.SaveChangesAsync(cancellationToken);
            }
            catch (Exception ex)
            {
                response.IsSuccessful = false;
                response.ResponseType = ResponseType.Error;
                _logger.LogInformation("Branch succesfully saved");
            }
            return response;
        }
    }
}
