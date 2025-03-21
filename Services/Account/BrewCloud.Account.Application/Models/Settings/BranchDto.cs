﻿using System;

namespace BrewCloud.Account.Application.Models.Settings
{
    public class BranchDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string District { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
    }
}
