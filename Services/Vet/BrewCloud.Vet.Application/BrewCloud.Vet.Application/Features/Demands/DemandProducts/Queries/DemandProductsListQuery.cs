using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BrewCloud.Shared.Dtos;
using BrewCloud.Shared.Service;
using BrewCloud.Vet.Application.Models.Demands.DemandProducts;
using BrewCloud.Vet.Domain.Contracts;

namespace BrewCloud.Vet.Application.Features.Demands.DemandProducts.Queries
{

    public class DemandProductsListQuery : IRequest<Response<List<DemandProductsDto>>>
    {
    }

    public class DemandProductsListQueryHandler : IRequestHandler<DemandProductsListQuery, Response<List<DemandProductsDto>>>
    {

        private readonly IIdentityRepository _identityRepository;
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;

        public DemandProductsListQueryHandler(IIdentityRepository identityRepository, IUnitOfWork uow, IMapper mapper)
        {
            _identityRepository = identityRepository;
            _uow = uow;
            _mapper = mapper;
        }
        public async Task<Response<List<DemandProductsDto>>> Handle(DemandProductsListQuery request, CancellationToken cancellationToken)
        {
            var response = new Response<List<DemandProductsDto>>();
            try
            {
                string query = "Select * from vetDemandProducts where Deleted = 0 order by CreateDate desc";
                var _data = _uow.Query<DemandProductsDto>(query).ToList();
                response = new Response<List<DemandProductsDto>>
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
