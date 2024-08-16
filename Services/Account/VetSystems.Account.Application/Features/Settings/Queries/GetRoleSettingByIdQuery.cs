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
    public class GetRoleSettingByIdQuery : IRequest<Response<List<RoleSettingDto>>>
    {
        public Guid Id { get; set; }
    }

    public class GetRoleSettingByIdQueryHandler : IRequestHandler<GetRoleSettingByIdQuery, Response<List<RoleSettingDto>>>
    {
        private readonly IRepository<Rolesetting> _roleSettingRepository;
        private readonly IRepository<RoleSettingDetail> _roleSettingDetailRepository;
        private readonly IIdentityRepository _identityRepository;
        private readonly IMapper _mapper;

        public GetRoleSettingByIdQueryHandler(IRepository<Rolesetting> roleSettingRepository, IIdentityRepository identityRepository, IMapper mapper, IRepository<RoleSettingDetail> roleSettingDetailRepository)
        {
            _roleSettingRepository = roleSettingRepository ?? throw new ArgumentNullException(nameof(roleSettingRepository));
            _identityRepository = identityRepository ?? throw new ArgumentNullException(nameof(identityRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _roleSettingDetailRepository = roleSettingDetailRepository;
        }

        public async Task<Response<List<RoleSettingDto>>> Handle(GetRoleSettingByIdQuery request, CancellationToken cancellationToken)
        {
            var rolesettings = await _roleSettingRepository.GetAsync(e => e.Id == request.Id && !e.Deleted);
            List<RoleSettingDetail> roleSettingDetails = _roleSettingDetailRepository.Get(p => p.RoleSettingId == request.Id && p.Deleted==false).ToList();

            List<RoleSettingDto> result = _mapper.Map<List<RoleSettingDto>>(rolesettings.OrderByDescending(e => e.CreateDate));
            List<RoleSettingDetailDto> roleSettingDetailDtos = _mapper.Map<List<RoleSettingDetailDto>>(roleSettingDetails.OrderByDescending(e => e.CreateDate));

            result[0].RoleSettingDetails = roleSettingDetailDtos;

            return Response<List<RoleSettingDto>>.Success(result, 200);
        }
    }
}
