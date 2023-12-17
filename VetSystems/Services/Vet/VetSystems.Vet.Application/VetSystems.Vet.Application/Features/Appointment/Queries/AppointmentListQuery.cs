using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VetSystems.Shared.Dtos;
using VetSystems.Shared.Service;
using VetSystems.Vet.Application.Models.Appointments;
using VetSystems.Vet.Domain.Contracts;

namespace VetSystems.Vet.Application.Features.Appointment.Queries
{
    public class AppointmentsListQuery : IRequest<Response<List<AppointmentsListDto>>>
    {
    }

    public class AppointmentsListQueryHandler : IRequestHandler<AppointmentsListQuery, Response<List<AppointmentsListDto>>>
    {
        private readonly IIdentityRepository _identityRepository;
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;

        public AppointmentsListQueryHandler(IIdentityRepository identityRepository, IUnitOfWork uow, IMapper mapper)
        {
            _identityRepository = identityRepository;
            _uow = uow;
            _mapper = mapper;
        }

        public async Task<Response<List<AppointmentsListDto>>> Handle(AppointmentsListQuery request, CancellationToken cancellationToken)
        {
            var response = new Response<List<AppointmentsListDto>>();
            try
            {
                string query = "SELECT       (vetcustomers.firstname) + ' ' + (vetcustomers.lastname) + ' - ' +  CASE vetappointments.appointmenttype\r\n        WHEN 1 THEN 'Aşı Randevusu'\r\n        WHEN 2 THEN 'Genel Muayene'\r\n        WHEN 3 THEN 'Kontrol Muayene'\r\n        WHEN 4 THEN 'Operasyon'\r\n        WHEN 5 THEN 'Tıraş'\r\n        WHEN 6 THEN 'Tedavi'\r\n        ELSE 'Diğer'\r\n    END AS text, vetappointments.begindate as startDate, vetappointments.enddate\r\nFROM            vetappointments INNER JOIN\r\n                         vetcustomers ON vetappointments.customerid = vetcustomers.id\r\n\t\t\t\t\t\t where vetappointments.deleted = 0";
                var _data = _uow.Query<AppointmentsListDto>(query).ToList();
                response = new Response<List<AppointmentsListDto>>
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
