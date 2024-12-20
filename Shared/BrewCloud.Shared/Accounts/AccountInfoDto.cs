﻿using System;
using System.Collections.Generic;
using System.Text;

namespace BrewCloud.Shared.Accounts
{
    public class AccountInfoDto
    {
        public string UserName { get; internal set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Email { get; set; }
        public Guid EnterpriseId { get; set; }
        public string Timezone { get; set; }
        public Guid UserId { get; set; }
        public AccountType AccountType { get; set; }
        public Guid TenantId { get; set; }
        public string ConnectionDb { get; set; }
        public string Host { get; set; }
        public Guid RoleId { get; set; }
        public int UseSafeListControl { get; set; }
        public int SubscriptionType { get; set; }
        public string CurrencyCode { get; set; }
        public string DefaultLanguage { get; set; }
    }

    public enum AccountType : int
    {
        None = 0,
        CompanyAdmin = 1,
        User = 2,
        Admin = 3,
        B2BSale = 4,
        B2BAgency = 5,
        B2BPerson = 6,
        B2BOperator = 7,
        B2BHotel = 8,
        B2BCustomer = 9
    }
}
