using System;
using System.Collections.Generic;
using System.Text;

namespace VetSystems.Shared.Accounts
{
    public class Tenant
    {

        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Host { get; set; }
        public string DatabaseConnectionString { get; set; }

        //public Tenant()
        //{
        //    _tenant = new TenantDbSettings();
        //}
        //public Guid Id { get; set; }
        //public string Company { get; set; }
        //public TenantDbSettings _tenant { get; set; }
        //public string ConnectionString 
        //{ 
        //    get { return Connection(); } 
        //    set { ConnectionString = value; } 
        //}

        //public string Connection()
        //{
        //    return $"Server={_tenant.ServerName};Database={Id + "_" + Company};User Id={_tenant.User};Password={_tenant.Password};";  
        //}

    }
}
