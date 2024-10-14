using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrewCloud.Account.Application.Models.Settings
{
    public class RoleSettingDto
    {
        public Guid Id { get; set; }
        public string Rolecode { get; set; }
        public DateTime CreatedDate { get; set; }
        public string DashboardPath { get; set; }
        public bool Deleted { get; set; } = false;
        public bool Installdevice { get; set; }
        public bool IsEnterpriseAdmin { get; set; }
        public List<RoleSettingDetailDto> RoleSettingDetails { get; set; }
    }
}
