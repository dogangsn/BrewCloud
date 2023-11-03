using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VetSystems.Shared.Dtos;
using VetSystems.Shared.Service;
using VetSystems.Vet.Application.Models.Demands.Demands;
using VetSystems.Vet.Domain.Contracts;

namespace VetSystems.Vet.Application.Features.Demands.Demand.Queries
{

    public class DemandListQuery : IRequest<Response<List<DemandsDto>>>
    {
    }

    public class DemandListQueryHandler : IRequestHandler<DemandListQuery, Response<List<DemandsDto>>>
    {

        private readonly IIdentityRepository _identityRepository;
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;

        public DemandListQueryHandler(IIdentityRepository identityRepository, IUnitOfWork uow, IMapper mapper)
        {
            _identityRepository = identityRepository;
            _uow = uow;
            _mapper = mapper;
        }
        public async Task<Response<List<DemandsDto>>> Handle(DemandListQuery request, CancellationToken cancellationToken)
        {
            var response = new Response<List<DemandsDto>>();
            try
            {
                string query = "Select * from vetDemands where Deleted = 0 and iscomplated = 0  order by CreateDate desc";
                var _data = _uow.Query<DemandsDto>(query).ToList();
                response = new Response<List<DemandsDto>>
                {
                    Data = _data,
                    IsSuccessful = true,
                };
            }
            catch (Exception ex)
            {
                response.IsSuccessful = false;
                //response.Errors = ex.ToString();
            }

            return response;


        }
    }
}
