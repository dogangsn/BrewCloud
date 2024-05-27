using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VetSystems.Vet.Application.Models.Dashboards
{
    public class DashboardsDto
    {
        public DashboardsDto()
        {
            TotalCount = new DashboardCountTotal();
        }
        public DashboardCountTotal? TotalCount { get; set; }

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
    }

}
