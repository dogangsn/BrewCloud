using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VetSystems.Shared.Dtos;
using VetSystems.Vet.Application.Models.Definition.ProductCategories;

namespace VetSystems.Vet.Application.Features.Definition.ProductCategory.Queries
{
    public class ProductCategoryListQuery : IRequest<Response<List<ProductCategoriesListDto>>>
    {
    }

    public class ProductCategoryListQueryHandler : IRequestHandler<ProductCategoryListQuery, Response<List<ProductCategoriesListDto>>>
    {
        public Task<Response<List<ProductCategoriesListDto>>> Handle(ProductCategoryListQuery request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
