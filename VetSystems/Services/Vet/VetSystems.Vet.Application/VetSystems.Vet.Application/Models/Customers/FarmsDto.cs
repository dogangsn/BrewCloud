﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VetSystems.Vet.Application.Models.Customers
{
    public class FarmsDto
    {
        public Guid? CustomerId { get; set; }
        public string FarmName { get; set; } = string.Empty;
        public string FarmContact { get; set; } = string.Empty;
        public string FarmRelationship { get; set; } = string.Empty;
        public bool? Active { get; set; }

    }

}
