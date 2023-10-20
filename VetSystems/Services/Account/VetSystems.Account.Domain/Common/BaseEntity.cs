using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VetSystems.Account.Domain.Common
{
    public class BaseEntity
    {
        public Guid Id { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime? UpdateDate { get; set; } = new DateTime(1900, 1, 1);
        public string CreateUser { get; set; } = string.Empty;
        public string UpdateUser { get; set; } = string.Empty;
        public bool Deleted { get; set; } = false;
    }
}
