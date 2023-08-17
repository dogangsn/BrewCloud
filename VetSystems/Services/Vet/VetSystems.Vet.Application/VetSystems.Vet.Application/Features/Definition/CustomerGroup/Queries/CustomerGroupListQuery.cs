using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VetSystems.Shared.Dtos;
using VetSystems.Shared.Service;
using VetSystems.Vet.Application.Models.Definition.CustomerGroup;
using VetSystems.Vet.Application.Models.Definition.ProductCategories;
using VetSystems.Vet.Domain.Contracts;

namespace VetSystems.Vet.Application.Features.Definition.CustomerGroup.Queries
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
                string query = "Select * from CustomerGroupDef where Deleted = 0";
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
