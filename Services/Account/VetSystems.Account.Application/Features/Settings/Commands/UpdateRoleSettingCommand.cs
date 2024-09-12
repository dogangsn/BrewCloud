using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using VetSystems.Account.Application.GrpServices;
using VetSystems.Account.Application.Models.Settings;
using VetSystems.Account.Domain.Contracts;
using VetSystems.Account.Domain.Entities;
using VetSystems.Shared.Dtos;
using VetSystems.Shared.Service;

namespace VetSystems.Account.Application.Features.Settings.Commands
{
    public class UpdateRoleSettingCommand : IRequest<Response<bool>>
    {
        public string RoleOwnerId { get; set; }
        public string Rolecode { get; set; }
        public string MainPage { get; set; }
        public List<SelectedNavigationDto> SelectedNavigations { get; set; }
        public List<SelectedActionsDto> SelectedActions { get; set; }
    }

    public class UpdateRoleSettingCommandHandler : IRequestHandler<UpdateRoleSettingCommand, Response<bool>>
    {

        private readonly IIdentityRepository _identity;
        private readonly IdentityGrpService _identityGrpService;
        private readonly ILogger<UpdateRoleSettingCommandHandler> _logger;
        private readonly IRepository<Rolesetting> _roleSettingRepository;
        private readonly IRepository<RoleSettingDetail> _roleSettingDetailRepository;
        private readonly IRepository<User> _userrepository;
        private readonly IUnitOfWork _uow;

        public UpdateRoleSettingCommandHandler(IIdentityRepository identity, IdentityGrpService identityGrpService, ILogger<UpdateRoleSettingCommandHandler> logger, IRepository<Domain.Entities.Userauthorization> userAuthorizationRepository, IUnitOfWork uow, IRepository<User> userrepository, IRepository<Rolesetting> roleSettingRepository, IRepository<RoleSettingDetail> roleSettingDetailRepository)
        {
            _identity = identity;
            _identityGrpService = identityGrpService;
            _logger = logger;
            _uow = uow ?? throw new ArgumentNullException(nameof(uow));
            _userrepository = userrepository;
            _roleSettingRepository = roleSettingRepository;
            _roleSettingDetailRepository = roleSettingDetailRepository;
        }


        public async Task<Response<bool>> Handle(UpdateRoleSettingCommand request, CancellationToken cancellationToken)
        {

            try
            {
                var enterpriseId = _identity.Account.EnterpriseId;

                var roleSettingDetails = _roleSettingDetailRepository.Get(p=>p.RoleSettingId == Guid.Parse(request.RoleOwnerId)).ToList();

                var selectedActionTargets = new HashSet<string>(request.SelectedActions.Select(a => a.Target));

                foreach (var item in roleSettingDetails)
                {
                    if (!selectedActionTargets.Contains(item.Target))
                    {
                        // Eğer item SelectedActions'da yoksa, Deleted'ı true yap
                        item.Deleted = true;
                    }
                    else
                    {
                        var action = request.SelectedActions.FirstOrDefault(p => p.Target == item.Target);
                        if (action != null)
                        {
                            item.Action = action.ToString();
                        }
                    }
                }

                // SelectedActions'da olup roleSettingDetails'da olmayan yeni öğeleri ekle
                var existingTargets = new HashSet<string>(roleSettingDetails.Where(r => !r.Deleted).Select(r => r.Target));
                var newItems = request.SelectedActions
                    .Where(a => !existingTargets.Contains(a.Target))
                    .Select(a => new RoleSettingDetail
                    {
                        Target = a.Target,
                        Action = a.ToString(),
                        Deleted = false
                    });

                roleSettingDetails.AddRange(newItems);

                var roleSettingOwner = await _roleSettingRepository.GetByIdAsync(Guid.Parse(request.RoleOwnerId));
                if (roleSettingOwner != null)
                {
                    roleSettingOwner.Rolesettingdetails = roleSettingDetails;
                    roleSettingOwner.DashboardPath = request.MainPage;
                    roleSettingOwner.Rolecode = request.Rolecode;
                    roleSettingOwner.UpdateDate = DateTime.UtcNow;
                    roleSettingOwner.UpdateUser = _identity.Account.UserName;
                    _roleSettingRepository.Update(roleSettingOwner);
                }
                else
                {
                    return Response<bool>.Fail("Role setting not found", 404);
                }


                await _uow.SaveChangesAsync(cancellationToken);
            }
            catch (RpcException ex)
            {
                _logger.LogInformation($"user can not saved : {ex.Message}");
                return Response<bool>.Fail("user cannot saved ", 500);

            }
            return Response<bool>.Success(200);
        }
    }
}
