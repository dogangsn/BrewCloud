using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VetSystems.Shared.Dtos;
using VetSystems.Shared.Service;
using VetSystems.Vet.Application.Models.Definition.Product;
using VetSystems.Vet.Domain.Contracts;

namespace VetSystems.Vet.Application.Features.Definition.ProductDescription.Queries
{
    public class ProductDescriptionFiltersQuery : IRequest<Response<List<ProductDescriptionsDto>>>
    {
        public int ProductType { get; set; }
    }

    public class ProductDescriptionFiltersQueryHandler : IRequestHandler<ProductDescriptionFiltersQuery, Response<List<ProductDescriptionsDto>>>
    {
        private readonly IIdentityRepository _identityRepository;
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;

        public ProductDescriptionFiltersQueryHandler(IIdentityRepository identityRepository, IUnitOfWork uow, IMapper mapper)
        {
            _identityRepository = identityRepository;
            _uow = uow;
            _mapper = mapper;
        }

        public async Task<Response<List<ProductDescriptionsDto>>> Handle(ProductDescriptionFiltersQuery request, CancellationToken cancellationToken)
        {
            var response = new Response<List<ProductDescriptionsDto>>();
            try
            {
                string query = "Select * from VetProducts where Deleted = 0 and ProductTypeId = @ProductTypeId order by CreateDate ";
                var _data =  _uow.Query<ProductDescriptionsDto>(query, new { ProductTypeId= request.ProductType }).ToList();
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
