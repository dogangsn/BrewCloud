using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrewCloud.Vet.Application.Models.Clinicalstatistics
{
    public  class GraphicListDto
    {
        public string Name { get; set; }
        public int? RealType  { get; set; }
        public List<MonthList> Months { get; set; }
        public int? realDateYear { get; set; }
        public decimal? SumAlis { get; set; }
        public decimal? SumSatis { get; set; }
        public decimal? NetPriceSum { get; set; }
        public string types { get; set; }
        public decimal? KdvSum { get; set; }

    }
    public class MonthList
    {
        public decimal? Ocak { get; set; }
        public decimal? Subat { get; set; }
        public decimal? Mart { get; set; }
        public decimal? Nisan { get; set; }
        public decimal? Mayis { get; set; }
        public decimal? Haziran { get; set; }
        public decimal? Temmuz { get; set; }
        public decimal? Agustos { get; set; }
        public decimal? Eylul { get; set; }
        public decimal? Ekim { get; set; }
        public decimal? Kasim { get; set; }
        public decimal? Aralik { get; set; }
        public int? RealType { get; set; }

    }
}
