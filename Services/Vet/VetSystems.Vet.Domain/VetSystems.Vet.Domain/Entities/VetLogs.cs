using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VetSystems.Vet.Domain.Common;

namespace VetSystems.Vet.Domain.Entities
{
    public class VetLogs : BaseEntity
    {
        public Guid Id { get; set; }
        public DateTime Date { get; set; }
        public Guid UserId { get; set; }
        public string UserName { get; set; } = string.Empty;
        public string OldValue { get; set; } = string.Empty;
        public string NewValue { get; set; } = string.Empty;
        public string FieldName { get; set; } = string.Empty;
        public Guid TenantId { get; set; }
        public string TableName { get; set; } = string.Empty;
        public string MasterId { get; set; } = string.Empty;
    }
}
