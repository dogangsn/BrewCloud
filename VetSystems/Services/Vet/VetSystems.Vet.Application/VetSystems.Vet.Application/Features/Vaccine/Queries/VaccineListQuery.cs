using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VetSystems.Shared.Dtos;
using VetSystems.Vet.Application.Features.Vaccine.Commands;
using VetSystems.Vet.Application.Models.Vaccine;

namespace VetSystems.Vet.Application.Features.Vaccine.Queries
{
    public class VaccineListQuery : IRequest<Response<List<VaccineListDto>>>
    {
    }
    public class VaccineListQueryHandler : IRequestHandler<VaccineListQuery, Response<List<VaccineListDto>>>
    {

         
        public Task<Response<List<VaccineListDto>>> Handle(VaccineListQuery request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
