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
using BrewCloud.Vet.Application.Models.GeneralSettings.Users;
using BrewCloud.Vet.Application.Models.Vaccine;
using BrewCloud.Vet.Domain.Contracts;
using BrewCloud.Vet.Domain.Entities;

namespace BrewCloud.Vet.Application.Features.GeneralSettings.Users.Queries
{
    public class GetShortCutsQuery : IRequest<Response<List<ShortCutListDto>>>
    {
    }

    public class GetShortCutsQueryHandler : IRequestHandler<GetShortCutsQuery, Response<List<ShortCutListDto>>>
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

        public async Task<Response<List<ShortCutListDto>>> Handle(GetShortCutsQuery request, CancellationToken cancellationToken)
        {
             var response = new Response<List<ShortCutListDto>>();
            try
            {

                string query = "Select * from vetshortcut where Deleted = 0  order by CreateDate ";
                var _data = _uow.Query<ShortCutListDto>(query).ToList();
                response = new Response<List<ShortCutListDto>>
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
