﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BrewCloud.Vet.Domain.Common;

namespace BrewCloud.Vet.Domain.Entities
{
    public class VetVaccineCalendar : BaseEntity
    {
        [NotMapped]
        public int RecId { get; set; }
        public int AnimalType { get; set; }
        public DateTime VaccineDate { get; set; }
        public DateTime? VaccinationDate { get; set; }
        public bool IsDone { get; set; } = false;
        public bool IsAdd { get; set; } = false;
        public Guid PatientId { get; set; }
        public Guid CustomerId { get; set; }
        public Guid VaccineId { get; set; }
        public string VaccineName { get; set; } = string.Empty;
    }

}
