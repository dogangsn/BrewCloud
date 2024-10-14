using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BrewCloud.Shared.Dtos;
using BrewCloud.Shared.Service;
using BrewCloud.Vet.Application.Models.Customers;
using BrewCloud.Vet.Domain.Contracts;

namespace BrewCloud.Vet.Application.Features.Customers.Queries
{
    public class VetVetAnimalsTypeListQuery : IRequest<Response<List<VetVetAnimalsTypeListDto>>>
    {
    }

    public class VetVetAnimalsTypeListQueryHandler : IRequestHandler<VetVetAnimalsTypeListQuery, Response<List<VetVetAnimalsTypeListDto>>>
    {
        private readonly IIdentityRepository _identityRepository;
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;

        public VetVetAnimalsTypeListQueryHandler(IIdentityRepository identityRepository, IUnitOfWork uow, IMapper mapper)
        {
            _identityRepository = identityRepository;
            _uow = uow;
            _mapper = mapper;
        }

        public async Task<Response<List<VetVetAnimalsTypeListDto>>> Handle(VetVetAnimalsTypeListQuery request, CancellationToken cancellationToken)
        {
            var response = new Response<List<VetVetAnimalsTypeListDto>>();
            try
            {
                string query = "Select * from Vetanimalstype  With(NOLOCK)";
                var _data = _uow.Query<VetVetAnimalsTypeListDto>(query).ToList();
                response = new Response<List<VetVetAnimalsTypeListDto>>
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
