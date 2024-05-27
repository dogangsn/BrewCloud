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
using VetSystems.Vet.Application.Models.Definition.Product;
using VetSystems.Vet.Domain.Contracts;

namespace VetSystems.Vet.Application.Features.Definition.ProductDescription.Queries
{
    public class ProductDescriptionListQuery : IRequest<Response<List<ProductDescriptionsDto>>>
    {
    }

    public class ProductDescriptionListQueryHandler : IRequestHandler<ProductDescriptionListQuery, Response<List<ProductDescriptionsDto>>>
    {
        private readonly IIdentityRepository _identityRepository;
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;

        public ProductDescriptionListQueryHandler(IIdentityRepository identityRepository, IUnitOfWork uow, IMapper mapper)
        {
            _identityRepository = identityRepository;
            _uow = uow;
            _mapper = mapper;
        }

        public async Task<Response<List<ProductDescriptionsDto>>> Handle(ProductDescriptionListQuery request, CancellationToken cancellationToken)
        {
            var response = new Response<List<ProductDescriptionsDto>>();
            try
            {
                string query = "Select * from VetProducts where Deleted = 0";
                var _data = _uow.Query<ProductDescriptionsDto>(query).ToList();
                response = new Response<List<ProductDescriptionsDto>>
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
