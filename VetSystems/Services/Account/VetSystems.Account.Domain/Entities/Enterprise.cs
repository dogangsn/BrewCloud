using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VetSystems.Account.Domain.Common;

namespace VetSystems.Account.Domain.Entities
{
    public class Enterprise : BaseEntity
    {
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

        public virtual ICollection<Reason> Reasons { get; set; }
        public virtual ICollection<Abilitygroup> Abilitygroups { get; set; }
    }
}
