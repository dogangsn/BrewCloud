using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BrewCloud.Account.Domain.Common;

namespace BrewCloud.Account.Domain.Entities
{
    public class Enterprise : BaseEntity
    {
        public Enterprise()
        {
            Userauthorizations = new HashSet<Userauthorization>();
            Properties = new HashSet<Property>();
            Reasons = new HashSet<Reason>();
            Abilitygroups = new HashSet<Abilitygroup>();
        }
        public string Enterprisename { get; set; } = "";
        public string Phone { get; set; } = "";
        public string Currencycode { get; set; } = "";
        public string Defaultlanguage { get; set; } = "";
        public string Translationlanguage { get; set; } = "";
        public string Timezone { get; set; } = "";
        public Guid TimezoneownerdetailId { get; set; }
        public decimal? CustomerInvoiceInfoLimit { get; set; } = 500;

        public bool UseSafeListControl { get; set; }
        public bool CustomerSearchStatus { get; set; }
        public bool MoneyChange { get; set; } = false;

        public virtual ICollection<Property> Properties { get; set; }
        public virtual ICollection<Reason> Reasons { get; set; }
        public virtual ICollection<Abilitygroup> Abilitygroups { get; set; }
        public virtual ICollection<Userauthorization> Userauthorizations { get; set; }
    }
}
