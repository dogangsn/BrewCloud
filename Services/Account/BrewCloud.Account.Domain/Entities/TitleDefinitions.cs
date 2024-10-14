using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BrewCloud.Account.Domain.Common;

namespace BrewCloud.Account.Domain.Entities
{
    public class TitleDefinitions : BaseEntity
    {
        public string Name { get; set; }
        public string Remark { get; set; }
        public bool? IsAppointmentShow { get; set; }

    }
}
