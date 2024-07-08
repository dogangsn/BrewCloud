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
using VetSystems.Vet.Domain.Entities;

namespace VetSystems.Vet.Application.Features.GeneralSettings.Users.Queries
{
    public class GetShortCutsQuery : IRequest<Response<List<VetShortcut>>>
    {
    }

    public class GetShortCutsQueryHandler : IRequestHandler<GetShortCutsQuery, Response<List<VetShortcut>>>
    {

        private readonly IIdentityRepository _identityRepository;
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;

        public GetShortCutsQueryHandler(IIdentityRepository identityRepository, IUnitOfWork uow, IMapper mapper)
        {
            _identityRepository = identityRepository;
            _uow = uow;
            _mapper = mapper;
        }

        public async Task<Response<List<VetShortcut>>> Handle(GetShortCutsQuery request, CancellationToken cancellationToken)
        {
             var response = new Response<List<VetShortcut>>();
            try
            {

                string query = "Select * from vetshortcut where Deleted = 0  order by CreateDate ";
                var _data = _uow.Query<VetShortcut>(query).ToList();
                response = new Response<List<VetShortcut>>
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
