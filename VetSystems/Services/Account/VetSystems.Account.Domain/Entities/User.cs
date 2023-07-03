using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VetSystems.Account.Domain.Common;

namespace VetSystems.Account.Domain.Entities
{
    public class User : BaseEntity
    {
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Email { get; set; }
        public Guid RoleId { get; set; }
        public Guid EnterprisesId { get; set; }
        public bool Authorizeenterprise { get; set; }
    }
}
