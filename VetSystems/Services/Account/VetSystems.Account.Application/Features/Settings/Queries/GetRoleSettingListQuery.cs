using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using VetSystems.Account.Application.Models.Settings;
using VetSystems.Account.Domain.Contracts;
using VetSystems.Account.Domain.Entities;
using VetSystems.Shared.Dtos;
using VetSystems.Shared.Service;

namespace VetSystems.Account.Application.Features.Settings.Queries
{
    public class GetRoleSettingListQuery : IRequest<Response<List<RoleSettingDto>>>
    {
    }

    public class GetRoleSettingListQueryHandler : IRequestHandler<GetRoleSettingListQuery, Response<List<RoleSettingDto>>>
    {
        private readonly IRepository<Rolesetting> _roleSettingRepository;
        private readonly IIdentityRepository _identityRepository;
        private readonly IMapper _mapper;

        public GetRoleSettingListQueryHandler(IRepository<Rolesetting> roleSettingRepository, IIdentityRepository identityRepository, IMapper mapper)
        {
            _roleSettingRepository = roleSettingRepository ?? throw new ArgumentNullException(nameof(roleSettingRepository));
            _identityRepository = identityRepository ?? throw new ArgumentNullException(nameof(identityRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<Response<List<RoleSettingDto>>> Handle(GetRoleSettingListQuery request, CancellationToken cancellationToken)
        {
            Guid enterpriseId = _identityRepository.Account.EnterpriseId;
            var rolesettings = await _roleSettingRepository.GetAsync(e => e.EnterprisesId == enterpriseId && !e.Deleted);
            var result = _mapper.Map<List<RoleSettingDto>>(rolesettings.OrderByDescending(e => e.CreateDate));
            return Response<List<RoleSettingDto>>.Success(result, 200);
        }
    }
}
