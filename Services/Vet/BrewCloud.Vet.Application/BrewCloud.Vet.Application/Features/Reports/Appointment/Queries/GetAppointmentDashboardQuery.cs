using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BrewCloud.Shared.Dtos;
using BrewCloud.Shared.Service;
using BrewCloud.Vet.Application.Models.Report;
using BrewCloud.Vet.Domain.Contracts;
using BrewCloud.Vet.Domain.Entities;

namespace BrewCloud.Vet.Application.Features.Reports.Appointment.Queries
{
    public class GetAppointmentDashboardQuery : IRequest<Response<AppointmentDashboardDto>>
    {
    }

    public class GetAppointmentDashboardQueryHandler : IRequestHandler<GetAppointmentDashboardQuery, Response<AppointmentDashboardDto>>
    {
        private readonly IIdentityRepository _identityRepository;
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;
        private readonly IRepository<VetAppointments> _vetAppointmentsRepository;

        public GetAppointmentDashboardQueryHandler(IIdentityRepository identityRepository, IUnitOfWork uow, IMapper mapper, IRepository<VetAppointments> vetAppointmentsRepository)
        {
            _identityRepository = identityRepository;
            _uow = uow;
            _mapper = mapper;
            _vetAppointmentsRepository = vetAppointmentsRepository;
        }

        public async Task<Response<AppointmentDashboardDto>> Handle(GetAppointmentDashboardQuery request, CancellationToken cancellationToken)
        {
            var response = Response<AppointmentDashboardDto>.Success(200);
            try
            {
                response.Data = new AppointmentDashboardDto();

                var _appointments = await _vetAppointmentsRepository.GetAsync(x => x.Deleted == false);

                var now = DateTime.Now;
                var startOfWeek = now.StartOfWeek(DayOfWeek.Monday);
                var startOfMonth = new DateTime(now.Year, now.Month, 1);
                var startOfYear = new DateTime(now.Year, 1, 1);

                var appointmentsThisWeek = _appointments.Count(x => x.BeginDate >= startOfWeek && x.BeginDate <= now);
                var appointmentsThisMonth = _appointments.Count(x => x.BeginDate >= startOfMonth && x.BeginDate <= now);
                var appointmentsThisYear = _appointments.Count(x => x.BeginDate >= startOfYear && x.BeginDate <= now);
                var completedAppointments = _appointments.Count(x => x.IsCompleted.GetValueOrDefault());

                var monthlyAppointmentCounts = new int[12];
                var MonthlyAppointmentCompletedCounts = new int[12];
                for (int i = 1; i <= 12; i++)
                {
                    var startOfSpecifiedMonth = new DateTime(now.Year, i, 1);
                    var endOfSpecifiedMonth = startOfSpecifiedMonth.AddMonths(1).AddDays(-1);
                    monthlyAppointmentCounts[i - 1] = _appointments.Count(x => x.BeginDate >= startOfSpecifiedMonth && x.BeginDate <= endOfSpecifiedMonth);
                    MonthlyAppointmentCompletedCounts[i - 1] = _appointments.Count(x => x.BeginDate >= startOfSpecifiedMonth && x.BeginDate <= endOfSpecifiedMonth && x.IsCompleted.GetValueOrDefault());
                }

                response.Data.TotalAppointmentWeek = appointmentsThisWeek;
                response.Data.TotalAppointmentMonth = appointmentsThisMonth;
                response.Data.TotalAppointmentYear = appointmentsThisYear;
                response.Data.TotalCompletedAppointments = completedAppointments;
                response.Data.MonthlyAppointmentCounts = monthlyAppointmentCounts;
                response.Data.MonthlyAppointmentCompletedCounts = MonthlyAppointmentCompletedCounts;
            }
            catch (Exception ex)
            {
                return Response<AppointmentDashboardDto>.Fail(ex.Message, 400);
            }
            return response;

        }
 


    }

    public static class DateTimeExtensions
    {
        public static DateTime StartOfWeek(this DateTime dt, DayOfWeek startOfWeek)
        {
            int diff = (7 + (dt.DayOfWeek - startOfWeek)) % 7;
            return dt.AddDays(-1 * diff).Date;
        }
    }
}
