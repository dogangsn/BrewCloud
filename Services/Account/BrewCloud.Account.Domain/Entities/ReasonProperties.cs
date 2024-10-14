using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BrewCloud.Account.Domain.Common;

namespace BrewCloud.Account.Domain.Entities
{
    public class ReasonProperties : BaseEntity
    {
        public Guid ReasonId { get; set; }
        public Guid PropertyId { get; set; }
        public Guid EnterprisesId { get; set; }


        //public virtual Enterprise Enterprises { get; set; }

        //public virtual Reason Reason { get; set; }

        //public virtual Property Property { get; set; }
    }
}
