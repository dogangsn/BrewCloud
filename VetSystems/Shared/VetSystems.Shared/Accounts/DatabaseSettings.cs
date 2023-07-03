﻿using System;
using System.Collections.Generic;
using System.Text;

namespace VetSystems.Shared.Accounts
{
    public class DatabaseSettings : IDatabaseSettings
    {
        public string ConnectionString { get; set; }
        public string CollectionName { get; set; }
    }
}
