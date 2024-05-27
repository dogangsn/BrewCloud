using AutoMapper;
using MediatR;
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
using VetSystems.Shared.Accounts;
using VetSystems.Shared.Dtos;
using VetSystems.Shared.Service;
using static MassTransit.ValidationResultExtensions;

namespace VetSystems.Account.Application.Features.Settings.Commands
{
    public class GetCompanyQuery : IRequest<Response<CompanyDto>>
    {
    }

    public class GetCompanyQueryHandler : IRequestHandler<GetCompanyQuery, Response<CompanyDto>>
    {

        private readonly IdentityGrpService _identityGrpService;
        private readonly IIdentityRepository _identity;
        private readonly IMapper _mapper;
        private readonly IRepository<Company> _companyRepository;

        public GetCompanyQueryHandler(IdentityGrpService identityGrpService,IIdentityRepository identity, IMapper mapper, IRepository<Company> companyRepository)
        {
            _identityGrpService = identityGrpService ?? throw new ArgumentNullException(nameof(identityGrpService));
            _identity = identity ?? throw new ArgumentNullException(nameof(identity));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _companyRepository = companyRepository ?? throw new ArgumentNullException();
        }


        public async Task<Response<CompanyDto>> Handle(GetCompanyQuery request, CancellationToken cancellationToken)
        {

            var result = await _companyRepository.GetAllAsync();
            if (result == null)
            {
                return Response<CompanyDto>.Fail("Not Found", 404);
            }
            CompanyDto company = _mapper.Map<CompanyDto>(result.FirstOrDefault());

            return Response<CompanyDto>.Success(company, 200);
        }
    }
}
