using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VetSystems.Shared.Dtos;
using VetSystems.Vet.Application.Models.Definition.PrintTemplate;

namespace VetSystems.Vet.Application.Features.Definition.PrintTemplate.Queries
{
    public class GetPrintTemplateListQuery : IRequest<Response<List<PrintTemplateListDto>>>
    {

    }

    public class GetPrintTemplateListQueryHandler : IRequestHandler<GetPrintTemplateListQuery, Response<List<PrintTemplateListDto>>>
    {
        public Task<Response<List<PrintTemplateListDto>>> Handle(GetPrintTemplateListQuery request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
