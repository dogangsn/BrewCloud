using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BrewCloud.Shared.Dtos;
using BrewCloud.Shared.Service;
using BrewCloud.Vet.Application.Models.Definition.Taxis;
using BrewCloud.Vet.Domain.Contracts;
using BrewCloud.Vet.Domain.Entities;

namespace BrewCloud.Vet.Application.Features.Definition.Taxis.Queries
{
    public class GetTaxisListQuery : IRequest<Response<List<TaxisDto>>>
    {
    }

    public class GetTaxisListQueryHandler : IRequestHandler<GetTaxisListQuery, Response<List<TaxisDto>>>
    {
        private readonly IIdentityRepository _identityRepository;
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;
        private readonly IRepository<VetTaxis> _taxisRepository;

        public GetTaxisListQueryHandler(IIdentityRepository identityRepository, IUnitOfWork uow, IMapper mapper, IRepository<VetTaxis> taxisRepository)
        {
            _identityRepository = identityRepository;
            _uow = uow;
            _mapper = mapper;
            _taxisRepository = taxisRepository;
        }

        public async Task<Response<List<TaxisDto>>> Handle(GetTaxisListQuery request, CancellationToken cancellationToken)
        {
            var response = Response<List<TaxisDto>>.Success(200);
            try
            {
                List<VetTaxis> taxes = (await _taxisRepository.GetAsync(x => x.Deleted == false)).ToList();
                var result = _mapper.Map<List<TaxisDto>>(taxes.OrderByDescending(e => e.CreateDate));

                response.Data = result;
            }
            catch (Exception ex)
            {

            }
            return response;
        }
    }
}
