using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VetSystems.Shared.Dtos;
using VetSystems.Shared.Service;
using VetSystems.Vet.Application.Models.Definition.ProductCategories;
using VetSystems.Vet.Application.Models.Definition.UnitDefinitions;
using VetSystems.Vet.Domain.Contracts;

namespace VetSystems.Vet.Application.Features.Definition.UnitDefinitions.Queries
{
    public class UnitsListQuery : IRequest<Response<List<UnitsListDto>>>
    {
    }

    public class UnitsListQueryHandler : IRequestHandler<UnitsListQuery, Response<List<UnitsListDto>>>
    {
        private readonly IIdentityRepository _identityRepository;
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;

        public UnitsListQueryHandler(IIdentityRepository identityRepository, IUnitOfWork uow, IMapper mapper)
        {
            _identityRepository = identityRepository;
            _uow = uow;
            _mapper = mapper;
        }
        public async Task<Response<List<UnitsListDto>>> Handle(UnitsListQuery request, CancellationToken cancellationToken)
        {
            var response = new Response<List<UnitsListDto>>();
            try
            {
                string query = "Select * from units where Deleted = 0";
                var _data = _uow.Query<UnitsListDto>(query).ToList();
                response = new Response<List<UnitsListDto>>
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
