using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VetSystems.Shared.Dtos;
using VetSystems.Vet.Application.Models.PetHotels.Accomodation;

namespace VetSystems.Vet.Application.Features.PetHotels.Accomodation.Queries
{
    public class GetAccomodationListQuery : IRequest<Response<List<AccomodationListDto>>>
    {

    }

    public class GetAccomodationListQueryHandler : IRequestHandler<GetAccomodationListQuery, Response<List<AccomodationListDto>>>
    {




        public Task<Response<List<AccomodationListDto>>> Handle(GetAccomodationListQuery request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
