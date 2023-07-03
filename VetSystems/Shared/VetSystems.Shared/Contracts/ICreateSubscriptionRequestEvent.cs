using System;
using System.Collections.Generic;
using System.Text;

namespace VetSystems.Shared.Contracts
{
    public interface ICreateSubscriptionRequestEvent
    {
        Guid RecId { get; set; }
        string Username { get; set; }
        string Password { get; set; }
        string Company { get; set; }
        string Phone { get; set; }
        string Email { get; set; }
        string ActivationCode { get; set; }
        public string ConnectionString { get; set; }
    }
}
