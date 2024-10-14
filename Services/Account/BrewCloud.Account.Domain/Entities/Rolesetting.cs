using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BrewCloud.Account.Domain.Common;

namespace BrewCloud.Account.Domain.Entities
{
    public class Rolesetting : BaseEntity
    {
        public string Rolecode { get; set; }
        public Guid EnterprisesId { get; set; }
        public bool Installdevice { get; set; }
        public bool IsEnterpriseAdmin { get; set; }
        public string DashboardPath { get; set; }
        //public virtual Enterprise Enterprises { get; set; }
        public virtual ICollection<RoleSettingDetail> Rolesettingdetails { get; set; }
    }
}
