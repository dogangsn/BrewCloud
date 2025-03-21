﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrewCloud.Vet.Application.Models.Customers
{
    public class PayChartListDto
    {
        public Guid Id { get; set; }
        public DateTime Date { get; set; }
        public string? Operation { get; set; }
        public decimal? Credit { get; set; }
        public decimal? Debit { get; set; }
        public decimal? Paid { get; set; }
        public decimal TotalPaid { get; set; }
        public decimal? Total { get; set; }
        public Guid? AppointmentId { get; set; }
        public string PaymentName { get; set; }
        public Guid SaleBuyId { get; set; }
        public string Remark { get; set; }
        public int PaymetntId { get; set; }
    }
}
