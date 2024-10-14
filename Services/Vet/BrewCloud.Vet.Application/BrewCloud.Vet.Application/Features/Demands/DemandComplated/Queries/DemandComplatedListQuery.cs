using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BrewCloud.Shared.Dtos;
using BrewCloud.Shared.Service;
using BrewCloud.Vet.Application.Models.Demands.Demands;
using BrewCloud.Vet.Domain.Contracts;

namespace BrewCloud.Vet.Application.Features.Demands.DemandComplated.Queries
{

    public class DemandComplatedListQuery : IRequest<Response<List<DemandsDto>>>
    {
    }

    public class DemandListQueryHandler : IRequestHandler<DemandComplatedListQuery, Response<List<DemandsDto>>>
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
        public async Task<Response<List<DemandsDto>>> Handle(DemandComplatedListQuery request, CancellationToken cancellationToken)
        {
            var response = new Response<List<DemandsDto>>();
            try
            {
                string query = "Select * from vetDemands where Deleted = 0 and iscomplated = 1  order by UpdateDate desc";
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
