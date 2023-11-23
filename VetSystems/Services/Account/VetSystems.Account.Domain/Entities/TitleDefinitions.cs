using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VetSystems.Account.Domain.Common;

namespace VetSystems.Account.Domain.Entities
{
    public class TitleDefinitions : BaseEntity
    {
        public string Name { get; set; }
        public string Remark { get; set; }

    }
}
