using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VetSystems.Account.Domain.Common;

namespace VetSystems.Account.Domain.Entities
{
    public class TempAccount : BaseEntity
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string Company { get; set; }
        public string Phone { get; set; }
        public string ActivationCode { get; set; } = "";
        public bool? IsComplate { get; set; } = false;
    }
}
