using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BrewCloud.Shared.Dtos;
using BrewCloud.Shared.Service;
using BrewCloud.Vet.Application.Models.Definition.Suppliers;
using BrewCloud.Vet.Domain.Contracts;

namespace BrewCloud.Vet.Application.Features.Suppliers.Queries
{
    public class SuppliersListQuery : IRequest<Response<List<SuppliersListDto>>>
    {
    }

    public class SuppliersListQueryHandler : IRequestHandler<SuppliersListQuery, Response<List<SuppliersListDto>>>
    {

        private readonly IIdentityRepository _identityRepository;
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;

        public SuppliersListQueryHandler(IIdentityRepository identityRepository, IUnitOfWork uow, IMapper mapper)
        {
            _identityRepository = identityRepository;
            _uow = uow;
            _mapper = mapper;
        }
        public async Task<Response<List<SuppliersListDto>>> Handle(SuppliersListQuery request, CancellationToken cancellationToken)
         {
            var response = new Response<List<SuppliersListDto>>();
            try
            {
                string query = "Select * from vetSuppliers where Deleted = 0 order by CreateDate ";
                var _data = _uow.Query<SuppliersListDto>(query).ToList();
                response = new Response<List<SuppliersListDto>>
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
