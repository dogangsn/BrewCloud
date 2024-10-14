using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using BrewCloud.Account.Application.Models.Settings;
using BrewCloud.Account.Domain.Contracts;
using BrewCloud.Account.Domain.Entities;
using BrewCloud.Shared.Dtos;
using BrewCloud.Shared.Service;

namespace BrewCloud.Account.Application.Features.Settings.Queries
{
    public class GetUserRoleSettingListQuery : IRequest<Response<List<RoleSettingDto>>>
    {
    }

    public class GetUserRoleSettingListQueryHandler : IRequestHandler<GetUserRoleSettingListQuery, Response<List<RoleSettingDto>>>
    {
        private readonly IRepository<Rolesetting> _roleSettingRepository;
        private readonly IRepository<RoleSettingDetail> _roleSettingDetailRepository;
        private readonly IIdentityRepository _identityRepository;
        private readonly IMapper _mapper;

        public GetUserRoleSettingListQueryHandler(IRepository<Rolesetting> roleSettingRepository, IIdentityRepository identityRepository, IMapper mapper, IRepository<RoleSettingDetail> roleSettingDetailRepository)
        {
            _roleSettingRepository = roleSettingRepository ?? throw new ArgumentNullException(nameof(roleSettingRepository));
            _identityRepository = identityRepository ?? throw new ArgumentNullException(nameof(identityRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _roleSettingDetailRepository = roleSettingDetailRepository;
        }

        public async Task<Response<List<RoleSettingDto>>> Handle(GetUserRoleSettingListQuery request, CancellationToken cancellationToken)
        {
            Guid enterpriseId = _identityRepository.Account.EnterpriseId;
            var roleId = _identityRepository.Account.RoleId;
            var rolesettings = await _roleSettingRepository.GetAsync(e => e.EnterprisesId == enterpriseId && e.Id == roleId && !e.Deleted);
            List<RoleSettingDetail> roleSettingDetails = _roleSettingDetailRepository.Get(p => p.RoleSettingId == roleId).ToList();

            List<RoleSettingDto> result = _mapper.Map<List<RoleSettingDto>>(rolesettings.OrderByDescending(e => e.CreateDate));
            List<RoleSettingDetailDto> roleSettingDetailDtos = _mapper.Map<List<RoleSettingDetailDto>>(roleSettingDetails.OrderByDescending(e => e.CreateDate));

            result[0].RoleSettingDetails = roleSettingDetailDtos;

            return Response<List<RoleSettingDto>>.Success(result, 200);
        }
    }
}
