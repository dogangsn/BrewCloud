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
using BrewCloud.Vet.Application.Models.Definition.ProductCategories;
using BrewCloud.Vet.Domain.Contracts;

namespace BrewCloud.Vet.Application.Features.Definition.ProductCategory.Queries
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
                string query = "Select * from vetproductcategories where Deleted = 0";
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
