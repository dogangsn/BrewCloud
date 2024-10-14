using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using BrewCloud.Account.Application.Models.Settings;
using BrewCloud.Account.Domain.Contracts;
using BrewCloud.Account.Domain.Entities;
using BrewCloud.Shared.Dtos;
using BrewCloud.Shared.Service;

namespace BrewCloud.Account.Application.Features.Settings.Commands
{
    public class CreateRoleSettingCommand : IRequest<Response<bool>>
    {
        public string Rolecode { get; set; }
        public string MainPage { get; set; }
        public List<SelectedNavigationDto> SelectedNavigations { get; set; }
        public List<SelectedActionsDto> SelectedActions { get; set; }
    }

    public class CreateRoleSettingCommandHandler : IRequestHandler<CreateRoleSettingCommand, Response<bool>>
    {
        private readonly IUnitOfWork _uow;
        private readonly IRepository<Domain.Entities.Rolesetting> _roleSettingRepository;
        private readonly IRepository<Domain.Entities.RoleSettingDetail> _roleSettingDetailRepository;
        private readonly IIdentityRepository _identity;
        private readonly ILogger<CreateRoleSettingCommandHandler> _logger;

        public CreateRoleSettingCommandHandler(IUnitOfWork uow, IIdentityRepository identity, IRepository<Domain.Entities.Rolesetting> roleSettingRepository, ILogger<CreateRoleSettingCommandHandler> logger, IRepository<Domain.Entities.RoleSettingDetail> roleSettingDetailRepository)
        {
            _identity = identity ?? throw new ArgumentNullException(nameof(identity));
            _uow = uow ?? throw new ArgumentNullException(nameof(uow));
            _roleSettingRepository = roleSettingRepository ?? throw new ArgumentNullException(nameof(roleSettingRepository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _roleSettingDetailRepository = roleSettingDetailRepository ?? throw new ArgumentNullException(nameof(roleSettingDetailRepository));
        }

        public async Task<Response<bool>> Handle(CreateRoleSettingCommand request, CancellationToken cancellationToken)
        {
            var enterpriseId = _identity.Account.EnterpriseId;
            var userId = _identity.Account.UserId;

            var rolesetting = new Domain.Entities.Rolesetting
            {
                Installdevice = true,
                Rolecode = request.Rolecode,
                DashboardPath = "",
                EnterprisesId = enterpriseId,
                CreateDate =  DateTime.Now,
                CreateUser = userId.ToString(),
            };
            await _roleSettingRepository.AddAsync(rolesetting);
            await _uow.SaveChangesAsync(cancellationToken);

            request.SelectedNavigations.ForEach((SidebarNavigationDto) =>
            {
                _roleSettingDetailRepository.AddAsync(new RoleSettingDetail
                {
                    RoleSettingId = rolesetting.Id,
                    CreateDate = DateTime.Now,
                    CreateUser = _identity.Account.UserName,
                    Target = SidebarNavigationDto.Target,
                    Action = request.SelectedActions.Where(p=>p.Target==SidebarNavigationDto.Target).Select(x=>x.Action).FirstOrDefault() ?? ""
                });
            });
            await _uow.SaveChangesAsync(cancellationToken);

            _logger.LogInformation("Role Setting succesfully saved");
            return Response<bool>.Success(200);
        }
    }
}
