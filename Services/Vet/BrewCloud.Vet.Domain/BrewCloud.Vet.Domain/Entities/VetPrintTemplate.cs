using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BrewCloud.Vet.Domain.Common;

namespace BrewCloud.Vet.Domain.Entities
{
    public class VetPrintTemplate : BaseEntity
    {
        public string TemplateName { get; set; } = string.Empty;
        public PrintType Type { get; set; }
        public string HtmlContent { get; set; } = string.Empty;
    }

    public enum PrintType
    {
        customer = 1,
        patient = 2,
        Accomodation = 3,
        Farm = 4,
        Accounting = 5,
        Product = 6,
        Examination = 7,
    }

}
