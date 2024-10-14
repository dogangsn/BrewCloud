using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BrewCloud.Shared.Dtos;
using BrewCloud.Shared.Service;
using BrewCloud.Vet.Application.Models.Definition.CustomerGroup;
using BrewCloud.Vet.Application.Models.Definition.ProductCategories;
using BrewCloud.Vet.Domain.Contracts;

namespace BrewCloud.Vet.Application.Features.Definition.CustomerGroup.Queries
{
    public class CustomerGroupListQuery : IRequest<Response<List<CustomerGroupDefDto>>>
    {
    }

    public class CustomerGroupListQueryHandler : IRequestHandler<CustomerGroupListQuery, Response<List<CustomerGroupDefDto>>>
    {
        private readonly IIdentityRepository _identityRepository;
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;

        public CustomerGroupListQueryHandler(IIdentityRepository identityRepository, IUnitOfWork uow, IMapper mapper)
        {
            _identityRepository = identityRepository;
            _uow = uow;
            _mapper = mapper;
        }

        public async Task<Response<List<CustomerGroupDefDto>>> Handle(CustomerGroupListQuery request, CancellationToken cancellationToken)
        {
            var response = new Response<List<CustomerGroupDefDto>>();
            try
            {
                string query = "Select * from vetCustomerGroupDef where Deleted = 0";
                var _data = _uow.Query<CustomerGroupDefDto>(query).ToList();
                response = new Response<List<CustomerGroupDefDto>>
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
