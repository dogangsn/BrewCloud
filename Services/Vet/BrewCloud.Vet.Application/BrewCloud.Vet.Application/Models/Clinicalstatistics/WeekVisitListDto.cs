using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrewCloud.Vet.Application.Models.Clinicalstatistics
{
    public class WeekVisitListDto
    {
        public DateTime? BeginDate { get; set; }
        public int? AllVisitcount { get; set; }
        public int? VisitCountSum { get; set; }
        public int? UnVisitCountSum { get; set; }
        public string DayName { get; set; }
    }
}
