﻿using System;
using System.Collections.Generic;
using System.Text;

namespace BrewCloud.IdentityServer.Application.Models
{
    public class ExchangeRequestDto
    {
        public DateTime CurrDate { get; set; }
        public Guid PropertyId { get; set; }
        public string ConnectionString { get; set; }
    }
}
