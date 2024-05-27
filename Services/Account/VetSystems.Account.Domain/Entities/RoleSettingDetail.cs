using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VetSystems.Account.Domain.Common;

namespace VetSystems.Account.Domain.Entities
{
    public class RoleSettingDetail : BaseEntity
    {
        public string Target { get; set; }
        public string Action { get; set; }
        public Guid RoleSettingId { get; set; }
        public virtual Rolesetting Rolesetting { get; set; }
    }
}
