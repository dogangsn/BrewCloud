using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VetSystems.Shared.Dtos;
using VetSystems.Shared.Service;
using VetSystems.Vet.Application.Models.Customers;
using VetSystems.Vet.Domain.Contracts;

namespace VetSystems.Vet.Application.Features.Customers.Queries
{
    public class AnimalBreedsDefListQuery : IRequest<Response<List<VetAnimalBreedsDefDto>>>
    {
    }

    public class AnimalBreedsDefListQueryHandler : IRequestHandler<AnimalBreedsDefListQuery, Response<List<VetAnimalBreedsDefDto>>>
    {
        private readonly IIdentityRepository _identityRepository;
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;

        public AnimalBreedsDefListQueryHandler(IIdentityRepository identityRepository, IUnitOfWork uow, IMapper mapper)
        {
            _identityRepository = identityRepository;
            _uow = uow;
            _mapper = mapper;
        }

        public async Task<Response<List<VetAnimalBreedsDefDto>>> Handle(AnimalBreedsDefListQuery request, CancellationToken cancellationToken)
        {
            var response = new Response<List<VetAnimalBreedsDefDto>>();
            try
            {
                string query = "Select * from VetanimalBreedsdef  With(NOLOCK)";
                var _data = _uow.Query<VetAnimalBreedsDefDto>(query).ToList();
                response = new Response<List<VetAnimalBreedsDefDto>>
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
