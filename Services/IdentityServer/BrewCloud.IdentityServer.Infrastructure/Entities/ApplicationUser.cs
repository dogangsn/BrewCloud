using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Security.Principal;
using System.Text;

namespace BrewCloud.IdentityServer.Infrastructure.Entities
{
    public class ApplicationUser : IdentityUser
    {
        public virtual Accounts Account { get; set; }
        public string City { get; set; }
    }
}
