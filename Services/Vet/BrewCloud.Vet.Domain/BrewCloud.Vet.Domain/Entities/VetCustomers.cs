﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BrewCloud.Vet.Domain.Common;

namespace BrewCloud.Vet.Domain.Entities
{
    public class VetCustomers : BaseEntity, IAggregateRoot
    {
        public VetCustomers()
        {
            Patients = new HashSet<VetPatients>();
        }
         
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public string PhoneNumber2 { get; set; } = string.Empty;
        public string EMail { get; set; } = string.Empty;
        public string TaxOffice { get; set; } = string.Empty;
        public string VKNTCNo { get; set; } = string.Empty;
        public Guid? CustomerGroup { get; set; }
        public string Note { get; set; } = string.Empty;
        public decimal DiscountRate { get; set; } = 0;
        public bool? IsEmail { get; set; } = true;
        public bool? IsPhone { get; set; } = true;
        public bool? IsArchive { get; set; } = false;
        public VetAdress Adress { get; set; }
        public virtual ICollection<VetPatients> Patients { get; set; }

    }
}
