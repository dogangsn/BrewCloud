using System;
using System.Collections.Generic;
using System.Text;

namespace VetSystems.Shared.Accounts
{
    public interface IDatabaseSettings
    {
        public string CollectionName { get; set; }
        public string ConnectionString { get; set; }
    }
}
