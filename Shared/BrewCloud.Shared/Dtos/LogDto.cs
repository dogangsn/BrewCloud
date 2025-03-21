﻿using System;
using System.Collections.Generic;
using System.Text;

namespace BrewCloud.Shared.Dtos
{
    public class LogDto
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
