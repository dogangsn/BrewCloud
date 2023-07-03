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
    public class CustomersListQuery : IRequest<Response<List<CustomersDto>>>
    {
    }

    public class CustomersListQueryHandler : IRequestHandler<CustomersListQuery, Response<List<CustomersDto>>>
    {
        private readonly IIdentityRepository _identityRepository;
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;

        public CustomersListQueryHandler(IIdentityRepository identityRepository, IUnitOfWork uow, IMapper mapper)
        {
            _identityRepository = identityRepository;
            _uow = uow;
            _mapper = mapper;
        }

        public async Task<Response<List<CustomersDto>>> Handle(CustomersListQuery request, CancellationToken cancellationToken)
        {
            var response = new Response<List<CustomersDto>>();
            try
            {
                string query = "Select * from Customers";
                var _data = _uow.Query<CustomersDto>(query).ToList();
                response = new Response<List<CustomersDto>>
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
