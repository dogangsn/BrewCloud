using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BrewCloud.Account.Domain.Common;

namespace BrewCloud.Account.Domain.Entities
{
    public class User : BaseEntity
    {
        public bool Active { get; set; } = true;
        public Guid? Title { get; set; }
        public string Firstname { get; set; } = string.Empty;
        public string Lastname { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty; 
        public Guid RoleId { get; set; }
        public Guid EnterprisesId { get; set; }
        public bool Authorizeenterprise { get; set; }

    }
}
