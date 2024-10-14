using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrewCloud.Account.Application.Models.Settings
{
    public class RoleSettingDetailDto
    {
        public Guid Recid { get; set; }
        public string Target { get; set; }
        public string Action { get; set; }
        public Guid RoleSettingId { get; set; }
    }
}
