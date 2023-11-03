using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VetSystems.Chat.Infrastructure.Common
{
    public class BaseEntity : IdBaseEntity
    {
        public Guid Id { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime UpdateDate { get; set; }
        public DateTime DeletedDate { get; set; }
        public bool Deleted { get; set; }
        public string DeletedUsers { get; set; } = string.Empty;
        public string UpdateUsers { get; set; } = string.Empty;
        public string CreateUsers { get; set; } = string.Empty;
    }
}
