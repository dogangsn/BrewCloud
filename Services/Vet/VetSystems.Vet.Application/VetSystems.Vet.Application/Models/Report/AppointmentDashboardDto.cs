using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VetSystems.Vet.Application.Models.Report
{
    public class AppointmentDashboardDto
    {
        
        public int? TotalAppointmentWeek { get; set; }
        public int? TotalAppointmentMonth { get; set; }
        public int? TotalAppointmentYear { get; set; }
        public int? TotalCompletedAppointments { get; set; }
        public int[] MonthlyAppointmentCounts { get; set; }
        public int[] MonthlyAppointmentCompletedCounts { get; set; }

        public int TotalJanuary { get; } = 0;
        public int TotalFebruary { get; } = 0;
        public int TotalMarch { get; } = 0;
        public int TotalApril { get; } = 0;
        public int TotalMay { get; } = 0;
        public int TotalJune { get; } = 0;
        public int TotalJuly { get; } = 0;
        public int TotalAugust { get; } = 0;
        public int TotalSeptember { get; } = 0;
        public int TotalOctober { get; } = 0;
        public int TotalNovember { get; } = 0;
        public int TotalDecember { get; } = 0;
    }



}
