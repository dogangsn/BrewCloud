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
        public string UserName { get; set; }
        public string OldValue { get; set; }
        public string NewValue { get; set; }
        public string FieldName { get; set; }
        public Guid TenantId { get; set; }
        public string TableName { get; set; }
        public string MasterId { get; set; }
    }
}
