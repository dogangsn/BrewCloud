using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using VetSystems.Account.Application.GrpServices;
using VetSystems.Account.Domain.Contracts;
using VetSystems.Shared.Accounts;
using VetSystems.Shared.Dtos;
using VetSystems.Shared.Service;

namespace VetSystems.Account.Application.Features.Settings.Queries
{
    public class GetUsersListQuery : IRequest<Response<List<SignupDto>>>
    {
    }

    public class GetUsersListQueryHandler : IRequestHandler<GetUsersListQuery, Response<List<SignupDto>>>
    {
        private readonly IdentityGrpService _identityGrpService;
        private readonly IIdentityRepository _identity;
        private readonly IMapper _mapper;
        private readonly IRepository<Domain.Entities.Rolesetting> _roleSettingRepository;
        private readonly IRepository<Domain.Entities.User> _userRepository;

        public GetUsersListQueryHandler(IdentityGrpService identityGrpService, IRepository<Domain.Entities.Rolesetting> roleSettingRepository, IIdentityRepository identity, IMapper mapper, IRepository<Domain.Entities.User> userRepository)
        {
            _identityGrpService = identityGrpService ?? throw new ArgumentNullException(nameof(identityGrpService));
            _identity = identity ?? throw new ArgumentNullException(nameof(identity));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _roleSettingRepository = roleSettingRepository ?? throw new ArgumentNullException(nameof(roleSettingRepository));
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
        }

        public async Task<Response<List<SignupDto>>> Handle(GetUsersListQuery request, CancellationToken cancellationToken)
        {
            var enterpriseId = _identity.Account.EnterpriseId;

            var identityUsers = await _identityGrpService.GetCompanyUsersAsync(enterpriseId.ToString());
            if (!identityUsers.IsSuccess)
            {
                return Response<List<SignupDto>>.Success(200);
            }
            var response = identityUsers.Data.ToList();
            //if (request.AccountType == 4)
            //{
            //    response.RemoveAll(r => r.AccountType < 4);
            //}
            //else
            //{
            //    response.RemoveAll(r => r.AccountType > 3);
            //}
            var users = await _userRepository.GetAllAsync();
            var roleList = await _roleSettingRepository.GetAllAsync();
            var result = response.Select(r => new SignupDto
            {
                Id = r.Id,
                FirstName = r.FirstName,
                LastName = r.LastName,
                Email = r.Email,
                AppKey = r.AppKey,
                UserAppKey = r.UserAppKey,
                RoleName = r.Roleid != "" ? roleList.Where(x => x.Id == Guid.Parse(r.Roleid)).Select(x => x.Rolecode).FirstOrDefault() : "",
                RoleId = r.Roleid,
                Active = users.Where(x => x.Email == r.Email).FirstOrDefault().Active,
                TitleId  = users.Where(x=>x.Email == r.Email).FirstOrDefault().Title
                //AccountType = r.AccountType,
            }).ToList();

            return Response<List<SignupDto>>.Success(result, 200);
        }
    }

}
