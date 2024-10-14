using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BrewCloud.Vet.Domain.Common;


namespace BrewCloud.Vet.Domain.Entities
{
    public class VetAccomodationCheckOuts : BaseEntity
    {
        public Guid AccomodationId { get; set; } 
        public DateTime CheckinDate { get; set; }
        public DateTime CheckOutDate { get; set; }
        public Guid? SaleBuyId { get; set; } = Guid.Empty;
        public decimal AccomodationAmount { get; set; }
        public decimal CollectionAmount { get; set; }
        public int PaymentId { get; set; }
    }
}
