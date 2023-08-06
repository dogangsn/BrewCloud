using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VetSystems.Shared.Dtos;

namespace VetSystems.Vet.Application.Features.Definition.ProductCategory.Commands
{
    public class CreateProductCategoriesCommand : IRequest<Response<bool>>
    {
    }

    public class CreateProductCategoriesCommandHandler : IRequestHandler<CreateProductCategoriesCommand, Response<bool>>
    {



        public Task<Response<bool>> Handle(CreateProductCategoriesCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
