using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VetSystems.Shared.Dtos;
using VetSystems.Shared.Service;
using VetSystems.Vet.Application.Models.PetHotels.Accomodation;
using VetSystems.Vet.Application.Models.PetHotels.Rooms;
using VetSystems.Vet.Domain.Contracts;

namespace VetSystems.Vet.Application.Features.PetHotels.Accomodation.Queries
{
    public class GetAccomodationListQuery : IRequest<Response<List<AccomodationListDto>>>
    {

    }

    public class GetAccomodationListQueryHandler : IRequestHandler<GetAccomodationListQuery, Response<List<AccomodationListDto>>>
    {
        private readonly IIdentityRepository _identityRepository;
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;

        public GetAccomodationListQueryHandler(IIdentityRepository identityRepository, IUnitOfWork uow, IMapper mapper)
        {
            _identityRepository = identityRepository;
            _uow = uow;
            _mapper = mapper;
        }

        public async Task<Response<List<AccomodationListDto>>> Handle(GetAccomodationListQuery request, CancellationToken cancellationToken)
        {
            var response = Response<List<AccomodationListDto>>.Success(200);
            try
            {
                string query = "SELECT        vetcustomers.firstname + ' ' + vetcustomers.lastname AS customerName, vetrooms.roomname, vetaccomodation.roomid, vetaccomodation.customerid, vetaccomodation.patientsid, vetaccomodation.checkindate,  "
                            + "                          vetaccomodation.checkoutdate, vetaccomodation.accomodation, vetaccomodation.remark, vetaccomodation.createdate, vetaccomodation.updatedate, vetaccomodation.createusers, vetaccomodation.type,  "
                            + "                          vetaccomodation.id "
                            + " FROM            vetaccomodation LEFT OUTER JOIN "
                            + "                          vetrooms ON vetaccomodation.roomid = vetrooms.id LEFT OUTER JOIN "
                            + "                          vetcustomers ON vetaccomodation.customerid = vetcustomers.id "
                            + " WHERE        (vetaccomodation.deleted = 0)";

                response.Data = _uow.Query<AccomodationListDto>(query);
            }
            catch (Exception ex)
            {
                
            }
            return response;
        }
    }
}
