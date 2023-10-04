﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VetSystems.Vet.Domain.Common;

namespace VetSystems.Vet.Domain.Entities
{
    public class VetPaymentMethods : BaseEntity
    {
        [NotMapped]
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Remark { get; set; } = string.Empty;
    }
}
