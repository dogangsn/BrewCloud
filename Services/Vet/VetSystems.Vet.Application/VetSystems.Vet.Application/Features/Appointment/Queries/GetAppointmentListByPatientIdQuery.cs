using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VetSystems.Shared.Dtos;
using VetSystems.Vet.Application.Models.Appointments; 
using VetSystems.Shared.Service; 
using VetSystems.Vet.Domain.Contracts;
using VetSystems.Vet.Domain.Entities;
using AutoMapper;


namespace VetSystems.Vet.Application.Features.Appointment.Queries
{
    public class GetAppointmentListByPatientIdQuery : IRequest<Response<List<AppointmentDailyListDto>>>
    {
        public Guid? PatientId { get; set; }
    }

    public class GetAppointmentListByPatientIdQueryHandler : IRequestHandler<GetAppointmentListByPatientIdQuery, Response<List<AppointmentDailyListDto>>>
    {
        private readonly IIdentityRepository _identityRepository;
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;
        private readonly IRepository<VetParameters> _parametersRepository;

        public GetAppointmentListByPatientIdQueryHandler(IIdentityRepository identityRepository, IUnitOfWork uow, IMapper mapper, IRepository<VetParameters> parametersRepository)
        {
            _identityRepository = identityRepository;
            _uow = uow;
            _mapper = mapper;
            _parametersRepository = parametersRepository;
        }

        public async Task<Response<List<AppointmentDailyListDto>>> Handle(GetAppointmentListByPatientIdQuery request, CancellationToken cancellationToken)
        {
            var response = new Response<List<AppointmentDailyListDto>>();
            try
            {
                bool _isFirstInspection = false;

                VetParameters? _param = (await _parametersRepository.GetAllAsync()).FirstOrDefault();
                if (_param != null)
                {
                    _isFirstInspection = _param.IsFirstInspection.GetValueOrDefault();
                }
                string query = "SELECT  vetappointments.id, "
                                + " vetappointments.begindate as date, "
                                + " (vetcustomers.firstname) + ' ' + (vetcustomers.lastname) + ' / ' + (vetpatients.name) as customerPatientName,   "
                                + " CASE vetappointments.appointmenttype    "
                                + "                                                WHEN 0 THEN 'İlk Muayene'     "
                                + "                                                WHEN 1 THEN 'Aşı Randevusu' "
                                + "                                                WHEN 2 THEN 'Genel Muayene' "
                                + "                                                WHEN 3 THEN 'Kontrol Muayene' "
                                + "                                                WHEN 4 THEN 'Operasyon' "
                                + "                                                WHEN 5 THEN 'Tıraş' "
                                + "                                                WHEN 6 THEN 'Tedavi' "
                                + "                                                ELSE 'Diğer' "
                                + " 												END AS services  , vetappointments.status, CASE vetappointments.status WHEN 1 THEN 'Bekliyor' WHEN 2 THEN 'Iptal Edildi' WHEN 3 THEN 'Görüşüldü' WHEN 4 THEN 'Gelmedi' ELSE '' END as StatusName "
                                + " FROM            vetappointments  "
                                + " INNER JOIN vetcustomers ON vetappointments.customerid = vetcustomers.id "
                                + " LEFT JOIN vetpatients ON vetappointments.patientsid = vetpatients.id "
                                + " where vetappointments.deleted = 0 and vetappointments.patientsid=@patientsid ";

                var _data = _uow.Query<AppointmentDailyListDto>(query, new { patientsid = request.PatientId }).ToList();

                response = new Response<List<AppointmentDailyListDto>>
                {
                    Data = _data,
                    IsSuccessful = true,
                };
            }
            catch (Exception ex)
            {
                response.IsSuccessful = false; 
            }
            return response;
        }
    
    }
}
