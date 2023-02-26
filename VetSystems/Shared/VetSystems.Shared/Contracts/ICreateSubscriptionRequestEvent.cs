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

        string Firstname { get; set; }
        string Lastname { get; set; }
        string Company { get; set; }
        string Phone { get; set; }

        string Email { get; set; }
        string ActivationCode { get; set; }
        string TimeZone { get; set; }
        string EndOfTime { get; set; }
        string Language { get; set; }
        Guid TimeZoneOwnerId { get; set; }
        public string ConnectionString { get; set; }
        public Guid CorrelationId { get; set; }
    }
}
