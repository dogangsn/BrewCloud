using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using VetSystems.Account.Domain.Contracts;
using VetSystems.Shared.Dtos;
using VetSystems.Shared.Service;

namespace VetSystems.Account.Application.Features.Settings.Commands
{
    public class DeleteRoleSettingCommand : IRequest<Response<bool>>
    {
        public Guid Id { get; set; }
    }

    public class DeleteRoleSettingCommandHandler : IRequestHandler<DeleteRoleSettingCommand, Response<bool>>
    {

        private readonly IUnitOfWork _uow;
        private readonly IRepository<Domain.Entities.Rolesetting> _roleSettingRepository;
        private readonly IIdentityRepository _identity;
        private readonly ILogger<DeleteRoleSettingCommandHandler> _logger;
        public DeleteRoleSettingCommandHandler(IUnitOfWork uow, IIdentityRepository identity, IRepository<Domain.Entities.Rolesetting> roleSettingRepository, ILogger<DeleteRoleSettingCommandHandler> logger)
        {
            _identity = identity ?? throw new ArgumentNullException(nameof(identity));
            _uow = uow ?? throw new ArgumentNullException(nameof(uow));
            _roleSettingRepository = roleSettingRepository ?? throw new ArgumentNullException(nameof(roleSettingRepository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<Response<bool>> Handle(DeleteRoleSettingCommand request, CancellationToken cancellationToken)
        {
            var rolesetting = await _roleSettingRepository.GetByIdAsync(request.Id);
            if (rolesetting == null)
            {
                return Response<bool>.Fail("Role Setting not found", 404);
            }
            var userId = _identity.Account.UserId;
            rolesetting.UpdateUser = userId.ToString();
            rolesetting.UpdateDate = DateTime.Now;
            rolesetting.Deleted = true;
            _roleSettingRepository.Update(rolesetting);
            await _uow.SaveChangesAsync();
            _logger.LogInformation($"Role Setting succesfully deleted id = {request.Id}");
            return Response<bool>.Success(200);
        }

    }
}
