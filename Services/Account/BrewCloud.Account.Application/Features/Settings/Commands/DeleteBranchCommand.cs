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
    public class DeleteBranchCommand : IRequest<Response<bool>>
    {
        public Guid Id { get; set; }
    }

    public class DeleteBranchHandler : IRequestHandler<DeleteBranchCommand, Response<bool>>
    {
        private readonly IUnitOfWork _uow;
        private readonly IRepository<Branch> _branchRepository;
        private readonly IIdentityRepository _identity;
        private readonly ILogger<DeleteBranchHandler> _logger;

        public DeleteBranchHandler(IUnitOfWork uow, IIdentityRepository identity, ILogger<DeleteBranchHandler> logger, IRepository<Branch> branchRepository)
        {
            _identity = identity ?? throw new ArgumentNullException(nameof(identity));
            _uow = uow ?? throw new ArgumentNullException(nameof(uow));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _branchRepository = branchRepository ?? throw new ArgumentNullException(nameof(branchRepository));
        }

        public async Task<Response<bool>> Handle(DeleteBranchCommand request, CancellationToken cancellationToken)
        {
            var response = new Response<bool>
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
                    branch.Deleted = true;
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
