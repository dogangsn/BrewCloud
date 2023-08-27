using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VetSystems.Shared.Dtos;
using VetSystems.Shared.Service;
using VetSystems.Vet.Application.Models.Store;
using VetSystems.Vet.Domain.Contracts;

namespace VetSystems.Vet.Application.Features.Store.Queries
{
    public class StoreListQuery : IRequest<Response<List<StoreListDto>>>
    {
    }

    public class StoreListQueryHandler : IRequestHandler<StoreListQuery, Response<List<StoreListDto>>>
    {
        private readonly IIdentityRepository _identityRepository;
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;

        public StoreListQueryHandler(IIdentityRepository identityRepository, IUnitOfWork uow, IMapper mapper)
        {
            _identityRepository = identityRepository;
            _uow = uow;
            _mapper = mapper;
        }

        public async Task<Response<List<StoreListDto>>> Handle(StoreListQuery request, CancellationToken cancellationToken)
        {
            var response = new Response<List<StoreListDto>>();
            try
            {
                string query = "Select * from vetStores where Deleted = 0";
                var _data = _uow.Query<StoreListDto>(query).ToList();
                response = new Response<List<StoreListDto>>
                {
                    Data = _data,
                    IsSuccessful = true,
                };
            }
            catch (Exception ex)
            {
                response.IsSuccessful = false;
            }

            return response;
        }
    }
}
