using BrewCloud.Gym.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrewCloud.Gym.Domain.Entities
{
    public class GymSubscriptionPackage : BaseEntity
    {
        public Guid? BranchId { get; set; }
        public Guid BranchDefId { get; set; }
        public string Name { get; set; }
        public int SubscriptionDuration { get; set; }
        public double PackagePrice { get; set; }
        public Guid? GroupId { get; set; }
    }
}
