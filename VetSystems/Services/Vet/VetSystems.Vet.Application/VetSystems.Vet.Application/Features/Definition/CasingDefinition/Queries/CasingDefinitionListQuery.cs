using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VetSystems.Shared.Dtos;
using VetSystems.Shared.Service;
using VetSystems.Vet.Application.Models.Definition.CasingDefinition;
using VetSystems.Vet.Domain.Contracts;

namespace VetSystems.Vet.Application.Features.Definition.CasingDefinition.Queries
{
    public class CasingDefinitionListQuery : IRequest<Response<List<CasingDefinitionListDto>>>
    {
    }

    public class CasingDefinitionListQueryHandler : IRequestHandler<CasingDefinitionListQuery, Response<List<CasingDefinitionListDto>>>
    {

        private readonly IIdentityRepository _identityRepository;
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;

        public CasingDefinitionListQueryHandler(IIdentityRepository identityRepository, IUnitOfWork uow, IMapper mapper)
        {
            _identityRepository = identityRepository;
            _uow = uow;
            _mapper = mapper;
        }
        public async Task<Response<List<CasingDefinitionListDto>>> Handle(CasingDefinitionListQuery request, CancellationToken cancellationToken)
        {
            var response = new Response<List<CasingDefinitionListDto>>();
            try
            {
                string query = "Select * from casingdefinition where Deleted = 0";
                var _data = _uow.Query<CasingDefinitionListDto>(query).ToList();
                response = new Response<List<CasingDefinitionListDto>>
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
