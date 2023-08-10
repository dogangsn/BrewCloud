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
using VetSystems.Vet.Application.Models.Definition.ProductCategories;
using VetSystems.Vet.Domain.Contracts;

namespace VetSystems.Vet.Application.Features.Definition.ProductCategory.Queries
{
    public class ProductCategoryListQuery : IRequest<Response<List<ProductCategoriesListDto>>>
    {
    }

    public class ProductCategoryListQueryHandler : IRequestHandler<ProductCategoryListQuery, Response<List<ProductCategoriesListDto>>>
    {

        private readonly IIdentityRepository _identityRepository;
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;

        public ProductCategoryListQueryHandler(IIdentityRepository identityRepository, IUnitOfWork uow, IMapper mapper)
        {
            _identityRepository = identityRepository;
            _uow = uow;
            _mapper = mapper;
        }
        public async Task<Response<List<ProductCategoriesListDto>>> Handle(ProductCategoryListQuery request, CancellationToken cancellationToken)
        {
            var response = new Response<List<ProductCategoriesListDto>>();
            try
            {
                string query = "Select * from productcategories where Deleted = 0";
                var _data = _uow.Query<ProductCategoriesListDto>(query).ToList();
                response = new Response<List<ProductCategoriesListDto>>
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
