using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BrewCloud.Shared.Dtos;
using BrewCloud.Shared.Service;
using BrewCloud.Vet.Application.Models.Store;
using BrewCloud.Vet.Domain.Contracts;

namespace BrewCloud.Vet.Application.Features.Store.Queries
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
                string query = "Select * from vetStores where Deleted = 0 order by CreateDate ";
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
