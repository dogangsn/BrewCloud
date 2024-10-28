using AutoMapper;
using BrewCloud.Account.Application.GrpServices;
using BrewCloud.Account.Application.Models.Settings;
using BrewCloud.Account.Domain.Contracts;
using BrewCloud.Account.Domain.Entities;
using BrewCloud.Shared.Dtos;
using BrewCloud.Shared.Service;
using MediatR;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace BrewCloud.Account.Application.Features.Settings.Queries
{
    public class GetBranchListQuery : IRequest<Response<List<BranchDto>>>
    {
    }
    public class GetBranchListQueryHandler : IRequestHandler<GetBranchListQuery, Response<List<BranchDto>>>
    {
        private readonly IdentityGrpService _identityGrpService;
        private readonly IIdentityRepository _identity;
        private readonly IMapper _mapper;
        private readonly IRepository<Branch> _branchRepository;
        public GetBranchListQueryHandler(IdentityGrpService identityGrpService, IIdentityRepository identity, IMapper mapper, IRepository<Branch> branchRepository)
        {
            _identityGrpService = identityGrpService ?? throw new ArgumentNullException(nameof(identityGrpService));
            _identity = identity ?? throw new ArgumentNullException(nameof(identity));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _branchRepository = branchRepository ?? throw new ArgumentNullException();
        }
        public async Task<Response<List<BranchDto>>> Handle(GetBranchListQuery request, CancellationToken cancellationToken)
        {
            var response = new Response<List<BranchDto>>
            {
                ResponseType = ResponseType.Ok,
                IsSuccessful = true
            };
            try
            {
                var data = await _branchRepository.GetAsync(x=>x.Deleted == false);
                if (data == null)
                {
                    return Response<List<BranchDto>>.Fail("Not Found", 404);
                }
                List<BranchDto> branchList = _mapper.Map<List<BranchDto>>(data);
                response.Data = branchList;
            }
            catch (Exception ex)
            {
                response.IsSuccessful = false;
                response.ResponseType = ResponseType.Error;
            }
            return response;
        }
    }
}
