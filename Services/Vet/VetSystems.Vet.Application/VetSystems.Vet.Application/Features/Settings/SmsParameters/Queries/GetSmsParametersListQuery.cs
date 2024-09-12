using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks; 
using VetSystems.Shared.Service;
using VetSystems.Vet.Domain.Contracts;
using VetSystems.Vet.Application.Models.Settings.SmsParameters;
using VetSystems.Vet.Domain.Entities;
using VetSystems.Shared.Dtos;
using AutoMapper;

namespace VetSystems.Vet.Application.Features.Settings.SmsParameters.Queries
{
    public class GetSmsParametersListQuery : IRequest<Response<List<SmsParametersDto>>>
    {

    }

    public class GetSmsParametersListQueryHandler : IRequestHandler<GetSmsParametersListQuery, Response<List<SmsParametersDto>>>
    {
        private readonly IIdentityRepository _identityRepository;
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;
        private readonly IRepository<VetSmsParameters> _smsParametersRepository;

        public GetSmsParametersListQueryHandler(IIdentityRepository identityRepository, IUnitOfWork uow, IMapper mapper, IRepository<VetSmsParameters> smsParametersRepository)
        {
            _identityRepository = identityRepository;
            _uow = uow;
            _mapper = mapper;
            _smsParametersRepository = smsParametersRepository;
        }

        public async Task<Response<List<SmsParametersDto>>> Handle(GetSmsParametersListQuery request, CancellationToken cancellationToken)
        {

            var response = Response<List<SmsParametersDto>>.Success(200);
            try
            {
                List<VetSmsParameters> _smsparameters = (await _smsParametersRepository.GetAsync(x => x.Deleted == false && x.Active == true)).ToList();
                var result = _mapper.Map<List<SmsParametersDto>>(_smsparameters);
                response.Data = result;
            }
            catch (Exception ex)
            {
                return Response<List<SmsParametersDto>>.Fail(ex.Message, 400);
            }
            return response;

        }
    }
}
