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
using VetSystems.Vet.Domain.Entities;

namespace VetSystems.Vet.Application.Features.Customers.Queries
{
    public class GetFarmCustomersListQuery : IRequest<Response<List<FarmsDto>>>
    {
    }

    public class GetFarmCustomersListQueryHandler : IRequestHandler<GetFarmCustomersListQuery, Response<List<FarmsDto>>>
    {

        private readonly IIdentityRepository _identityRepository;
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;

        public GetFarmCustomersListQueryHandler(IIdentityRepository identityRepository, IUnitOfWork uow, IMapper mapper)
        {
            _identityRepository = identityRepository;
            _uow = uow;
            _mapper = mapper;
        }

        public async Task<Response<List<FarmsDto>>> Handle(GetFarmCustomersListQuery request, CancellationToken cancellationToken)
        {

            var response = new Response<List<FarmsDto>>();
            try
            {
                string query = "SELECT vetfarms.id, vetfarms.customerid, vetfarms.farmname, vetfarms.farmcontact, vetfarms.farmrelationship, vetfarms.active, vetcustomers.firstname, vetcustomers.lastname,  "
                       + "   vetcustomers.phonenumber "
                       + "   FROM            vetfarms INNER JOIN "
                       + "                            vetcustomers ON vetfarms.customerid = vetcustomers.id " 
                       + "   Where vetfarms.deleted = 0 ";

                var _data = _uow.Query<FarmsDto>(query).ToList();
                response.Data = _data;
            }
            catch (Exception ex)
            {
                return Response<List<FarmsDto>>.Fail(ex.Message, 400);
            }
            return response;

        }
    }
}
