﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BrewCloud.Vet.Application.Models.Customers;
using BrewCloud.Vet.Domain.Entities;

namespace BrewCloud.Vet.Application.Models.Appointments
{
    public class AppointmentsDto
    {

        public Guid Id { get; set; }
        public DateTime beginDate { get; set; }
        public DateTime endDate { get; set; }
        public string note { get; set; } = string.Empty;
        public Guid? doctorId { get; set; } 
        public Guid? customerId { get; set; }
        public int AppointmentType { get; set; }
        public string Text { get; set; } = string.Empty;
        public bool? IsComplated { get; set; }
        public Guid? VaccineId { get; set; }

    }
}
