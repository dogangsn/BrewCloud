using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BrewCloud.Shared.Service;
using BrewCloud.Vet.Domain.Contracts;
using BrewCloud.Vet.Application.Models.Settings.SmsParameters;
using BrewCloud.Vet.Domain.Entities;
using BrewCloud.Shared.Dtos;

namespace BrewCloud.Vet.Application.Features.Settings.SmsParameters.Queries
{
    public class GetSmsParametersIdByQuery : IRequest<Response<SmsParametersDto>>
    {
        public int SmsIntegrationType { get; set; }
    }

    public class GetSmsParametersIdByQueryHandler : IRequestHandler<GetSmsParametersIdByQuery, Response<SmsParametersDto>>
    {
        private readonly IIdentityRepository _identityRepository;
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;
        private readonly IRepository<VetSmsParameters> _smsParametersRepository;

        public GetSmsParametersIdByQueryHandler(IIdentityRepository identityRepository, IUnitOfWork uow, IMapper mapper, IRepository<VetSmsParameters> smsParametersRepository)
        {
            _identityRepository = identityRepository;
            _uow = uow;
            _mapper = mapper;
            _smsParametersRepository = smsParametersRepository;
        }

        public async Task<Response<SmsParametersDto>> Handle(GetSmsParametersIdByQuery request, CancellationToken cancellationToken)
        {
            var response = new Response<SmsParametersDto>();
            try
            {
                VetSmsParameters _smsparameters = (await _smsParametersRepository.GetAsync(x => x.Deleted == false && x.SmsIntegrationType == request.SmsIntegrationType)).FirstOrDefault();
                var result = _mapper.Map<SmsParametersDto>(_smsparameters);
                response.Data = result;
                response.IsSuccessful = true;
            }
            catch (Exception ex)
            {
                response.IsSuccessful = false;
            }
            return response;
            
        }
    }
}
