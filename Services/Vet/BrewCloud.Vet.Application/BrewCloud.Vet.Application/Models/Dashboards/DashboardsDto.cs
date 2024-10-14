using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BrewCloud.Vet.Application.Models.Appointments;

namespace BrewCloud.Vet.Application.Models.Dashboards
{
    public class DashboardsDto
    {
        public DashboardsDto()
        {
            TotalCount = new DashboardCountTotal();
            UpcomingAppointment = new List<AppointmentDailyListDto>();
            PastAppointment = new List<AppointmentDailyListDto>();
        }
        public DashboardCountTotal? TotalCount { get; set; }
        public List<AppointmentDailyListDto> UpcomingAppointment { get; set; } 
        public List<AppointmentDailyListDto> PastAppointment { get; set; } 

    }

    public class DashboardCountTotal
    {
        public int DailyAddAppointmentCount { get; set; } = 0;
        public int DailyAddAppointmentCompletedCount { get; set; } = 0;
        public int DailyAddCustomerCount { get; set; } = 0;
        public int DailyAddCustomerYestardayCount { get; set; }
        public decimal? DailyTurnoverAmount { get; set; } = 0;
        public decimal? DailyTurnoverPreviousAmount { get; set; } = 0;
        public decimal? TotalStockAmount { get; set; } = 0;
        public decimal? WaitingTotalAmount { get; set; } = 0;
    }

}
