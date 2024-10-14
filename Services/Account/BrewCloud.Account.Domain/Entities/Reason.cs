using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BrewCloud.Account.Domain.Common;

namespace BrewCloud.Account.Domain.Entities
{
    public class Reason : BaseEntity
    {
        public string Name { get; set; }
        public KindType Kind { get; set; }
        public ReasonType Type { get; set; }
        public Guid EnterprisesId { get; set; }
        public virtual Enterprise Enterprises { get; set; }
        public virtual ICollection<ReasonProperties> ReasonProperties { get; set; }
    }
    public enum ReasonType
    {
        Reson,
        PredefinedNote,
        PaymentNote,
        DiscountNote
    }
    public enum KindType
    {
        Returned = 0,
        Canceled = 1,
        Waste = 2,
        Refund = 3,
        Complimentary = 4, //İkram
        FreeRight = 5, //Ödenmez
        FreeBoard = 6
    }
}
