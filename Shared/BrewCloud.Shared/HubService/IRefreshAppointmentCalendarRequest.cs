using System;
using System.Collections.Generic;
using System.Text;

namespace BrewCloud.Shared.HubService
{
    public interface IRefreshAppointmentCalendarRequest
    {
        public Guid UserId { get; set; }
        public List<AppointmentCalendarDto> Appointments { get; set; }
    }

    public class AppointmentCalendarDto
    {
        public Guid Id { get; set; }
        public string Text { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
