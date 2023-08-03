using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VetSystems.Shared.Dtos;
using VetSystems.Vet.Application.Models.Definition.Product;

namespace VetSystems.Vet.Application.Features.Definition.ProductDescription.Queries
{
    public class ProductDescriptionListQuery : IRequest<Response<List<ProductDescriptionsDto>>>
    {
    }

    public class ProductDescriptionListQueryHandler : IRequestHandler<ProductDescriptionListQuery, Response<List<ProductDescriptionsDto>>>
    {


        public Task<Response<List<ProductDescriptionsDto>>> Handle(ProductDescriptionListQuery request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
