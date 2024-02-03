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
using VetSystems.Vet.Domain.Entities;

namespace VetSystems.Vet.Application.Features.Appointment.Queries
{
    public class AppointmentFindByIdListQuery : IRequest<Response<List<AppointmentsDto>>>
    {
        public string CustomerId { get; set; }
    }

    public class AppointmentFindByIdListQueryHandler : IRequestHandler<AppointmentFindByIdListQuery, Response<List<AppointmentsDto>>>
    {
        private readonly IIdentityRepository _identityRepository;
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;
        private readonly IRepository<Vet.Domain.Entities.VetAppointments> _AppointmentRepository;

        public AppointmentFindByIdListQueryHandler(IIdentityRepository identityRepository, IUnitOfWork uow, IMapper mapper, IRepository<Domain.Entities.VetAppointments> appointmentRepository)
        {
            _identityRepository = identityRepository ?? throw new ArgumentNullException(nameof(identityRepository));
            _uow = uow ?? throw new ArgumentNullException(nameof(_uow));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _AppointmentRepository = appointmentRepository;
        }

        public async Task<Response<List<AppointmentsDto>>> Handle(AppointmentFindByIdListQuery request, CancellationToken cancellationToken)
        {
            var response = Response<List<AppointmentsDto>>.Success(200);
            try
            {
                //List<VetAppointments> appointmentsList = (await _AppointmentRepository.GetAsync(x => x.Deleted == false && x.CustomerId == Guid.Parse(request.CustomerId))).ToList();
                //var result = _mapper.Map<List<AppointmentsDto>>(appointmentsList.OrderByDescending(e => e.CreateDate));

                string query = "   SELECT       vetappointments.id,vetappointments.begindate, vetappointments.enddate,vetappointments.note, ISNULL(vetappointments.IsCompleted, 0) as IsComplated ,vetappointments.appointmenttype, vetappointments.vaccineid,  \r\n" +
                                            "   CASE vetappointments.appointmenttype\r\n\t\tWHEN 0 THEN 'İlk Muayene'\r\n        " +
                                            " WHEN 1 THEN 'Aşı Randevusu'\r\n        " +
                                            " WHEN 2 THEN 'Genel Muayene'\r\n        " +
                                            " WHEN 3 THEN 'Kontrol Muayene'\r\n        " +
                                            " WHEN 4 THEN 'Operasyon'\r\n        " +
                                            " WHEN 5 THEN 'Tıraş'\r\n        " +
                                            " WHEN 6 THEN 'Tedavi'\r\n        " +
                                            " ELSE 'Diğer'\r\n    END AS text  " +
                                            " FROM            vetappointments INNER JOIN\r\n                         " +
                                            " vetcustomers ON vetappointments.customerid = vetcustomers.id\r\n\t\t\t\t\t\t " +
                                            " where vetappointments.deleted = 0 and vetappointments.customerid = @customerid and vetappointments.appointmenttype != 0";

                var _data = _uow.Query<AppointmentsDto>(query, new { customerid = Guid.Parse(request.CustomerId)}).ToList();
                response.Data = _data;
            }
            catch (Exception ex)
            {
                response.IsSuccessful = false;
            }
            return response;
        }
    }
}
