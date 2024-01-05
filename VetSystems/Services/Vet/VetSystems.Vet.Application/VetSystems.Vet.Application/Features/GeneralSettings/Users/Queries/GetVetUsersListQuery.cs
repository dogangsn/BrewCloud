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
using VetSystems.Vet.Application.Models.GeneralSettings.Users;
using VetSystems.Vet.Domain.Contracts;

namespace VetSystems.Vet.Application.Features.GeneralSettings.Users.Queries
{
    public class GetVetUsersListQuery : IRequest<Response<List<VetUsersDto>>>
    {
    }

    public class GetVetUsersListQueryHandler : IRequestHandler<GetVetUsersListQuery, Response<List<VetUsersDto>>>
    {

        private readonly IIdentityRepository _identityRepository;
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;

        public GetVetUsersListQueryHandler(IIdentityRepository identityRepository, IUnitOfWork uow, IMapper mapper)
        {
            _identityRepository = identityRepository;
            _uow = uow;
            _mapper = mapper;
        }

        public async Task<Response<List<VetUsersDto>>> Handle(GetVetUsersListQuery request, CancellationToken cancellationToken)
        {
             var response = new Response<List<VetUsersDto>>();
            try
            {

                string query = "Select * from users where Deleted = 0 and title is not null  order by CreateDate ";
                var _data = _uow.Query<VetUsersDto>(query).ToList();
                response = new Response<List<VetUsersDto>>
                {
                    Data = _data,
                    IsSuccessful = true,
                };

            }
            catch (Exception)
            {
                response.IsSuccessful = false;
            }
            return response;
        }
    }
}
