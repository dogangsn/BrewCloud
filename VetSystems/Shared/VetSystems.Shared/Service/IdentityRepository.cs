using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using System;
using System.Collections.Generic;
using System.Text;
using VetSystems.Shared.Accounts;

namespace VetSystems.Shared.Service
{
    public class IdentityRepository : IIdentityRepository
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        public IdentityRepository(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public string Host
        {
            get
            {
                string host = _httpContextAccessor.HttpContext.Request.Host.Value;
                return host;
            }
        }

        public string ClientIp
        {
            get
            {
                string ip = string.Empty;
                if (_httpContextAccessor.HttpContext.Request.Headers.ContainsKey("CF-Connecting-IP"))
                {
                    ip = _httpContextAccessor.HttpContext.Request.Headers["CF-Connecting-IP"];
                }
                else
                {
                    ip = _httpContextAccessor.HttpContext.Features.Get<IHttpConnectionFeature>()?.RemoteIpAddress.ToString();
                }

                if (_httpContextAccessor.HttpContext.Request.Headers.ContainsKey("VB-DeviceId"))
                {
                    ip = _httpContextAccessor.HttpContext.Request.Headers["VB-DeviceId"];
                }
                return ip;

            }
        }
        public AccountInfoDto Account
        {
            get
            {
                var account = new AccountInfoDto
                {
                    UserName = _httpContextAccessor.HttpContext.User.FindFirst("UserName")?.Value,
                    Firstname = _httpContextAccessor.HttpContext.User.FindFirst("Firstname")?.Value,
                    Lastname = _httpContextAccessor.HttpContext.User.FindFirst("Lastname")?.Value,
                    Email = _httpContextAccessor.HttpContext.User.FindFirst("Email")?.Value,
                    UserId = new Guid(_httpContextAccessor.HttpContext.User.FindFirst("id")?.Value),
                    EnterpriseId = new Guid(_httpContextAccessor.HttpContext.User.FindFirst("CompanyId")?.Value),
                    TenantId = new Guid(_httpContextAccessor.HttpContext.User.FindFirst("TenantId")?.Value),
                    ConnectionDb = _httpContextAccessor.HttpContext.User.FindFirst("ConnectionDb")?.Value,
                    CurrencyCode = _httpContextAccessor.HttpContext.User.FindFirst("CurrencyCode")?.Value,
                    DefaultLanguage = _httpContextAccessor.HttpContext.User.FindFirst("DefaultLanguage")?.Value,
                    Host = _httpContextAccessor.HttpContext.User.FindFirst("Host")?.Value,
                    Timezone = _httpContextAccessor.HttpContext.Request.Headers["Timezone"],
                };

                //if (_httpContextAccessor.HttpContext.User.FindFirst("Modules") != null)
                //{
                //    string value = _httpContextAccessor.HttpContext.User.FindFirst("Modules")?.Value;
                //    if (!string.IsNullOrEmpty(value))
                //        account.Modules = JsonConvert.DeserializeObject<List<ModuleDto>>(_httpContextAccessor.HttpContext.User.FindFirst("Modules")?.Value);
                //}
                if (_httpContextAccessor.HttpContext.User.FindFirst("RoleId") != null)
                    account.RoleId = new Guid(_httpContextAccessor.HttpContext.User.FindFirst("RoleId")?.Value);

                if (_httpContextAccessor.HttpContext.User.FindFirst("UseSafeListControl") != null)
                    account.UseSafeListControl = Convert.ToInt32(_httpContextAccessor.HttpContext.User.FindFirst("UseSafeListControl")?.Value);

                //if (_httpContextAccessor.HttpContext.User.FindFirst("SubscriptionType") != null)
                //    account.SubscriptionType = (SubscriptionType)Enum.Parse(typeof(SubscriptionType), _httpContextAccessor.HttpContext.User.FindFirst("SubscriptionType")?.Value);

                var accountType = Enum.TryParse(_httpContextAccessor.HttpContext.User.FindFirst("AccountType")?.Value, out AccountType accType);
                account.AccountType = accType;
                return account;
            }

        }

        public Guid TenantId
        {
            get
            {
                Guid guid = Guid.Empty;
                bool x = false;
                if (_httpContextAccessor.HttpContext != null && _httpContextAccessor.HttpContext.Request.Headers.ContainsKey("VB-Tenant"))
                {
                    guid = Guid.Parse(_httpContextAccessor.HttpContext.Request.Headers["VB-Tenant"]);
                    x = true;
                }
                if (!x && _httpContextAccessor.HttpContext != null && _httpContextAccessor.HttpContext.User != null && !string.IsNullOrEmpty(_httpContextAccessor.HttpContext.User.FindFirst("TenantId")?.Value))
                {
                    guid = Guid.Parse(_httpContextAccessor.HttpContext.User.FindFirst("TenantId")?.Value);
                    x = true;
                }

                return guid;
            }

        }
        private string connection;
        public string Connection
        {
            get
            {

                bool x = false;

                if (_httpContextAccessor.HttpContext != null && _httpContextAccessor.HttpContext.Request.Headers.ContainsKey("VB-Db"))
                {
                    connection = _httpContextAccessor.HttpContext.Request.Headers["VB-Db"];
                    x = true;
                }
                if (!x && _httpContextAccessor.HttpContext != null && _httpContextAccessor.HttpContext.User != null && !string.IsNullOrEmpty(_httpContextAccessor.HttpContext.User.FindFirst("ConnectionDb")?.Value))
                {
                    connection = _httpContextAccessor.HttpContext.User.FindFirst("ConnectionDb")?.Value;
                    x = true;
                }

                return connection;
            }
            set
            {
                connection = value;
            }
        }

    }
}
