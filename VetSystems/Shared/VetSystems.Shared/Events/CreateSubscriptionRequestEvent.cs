using System;
using System.Collections.Generic;
using System.Text;
using VetSystems.Shared.Contracts;

namespace VetSystems.Shared.Events
{
    public class CreateSubscriptionRequestEvent : IntegrationBaseEvent, ICreateSubscriptionRequestEvent
    {
        public Guid RecId { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Company { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string ActivationCode { get; set; }
        public string ConnectionString { get; set; }
        public bool IsFirstCreate { get; set; }
        //public string TargetMigration { get; set; }
        //public string HistoryTable { get; set; }
        //public bool MoveHistoryTable { get; set; }


    }
}
