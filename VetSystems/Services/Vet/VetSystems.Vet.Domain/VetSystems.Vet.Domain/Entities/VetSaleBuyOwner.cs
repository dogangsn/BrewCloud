using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VetSystems.Vet.Domain.Common;

namespace VetSystems.Vet.Domain.Entities
{
    public class VetSaleBuyOwner : BaseEntity
    {
        public VetSaleBuyOwner()
        {
            VetSaleBuyTrans = new HashSet<VetSaleBuyTrans>();
        }
        public int Type { get; set; }
        public Guid? CustomerId { get; set; }
        public DateTime Date { get; set; }
        public string InvoiceNo { get; set; } = string.Empty;
        public int PaymentType { get; set; } 
        public decimal? Total { get; set; } = 0; 
        public decimal? Discount { get; set; } = 0; 
        public decimal? KDV { get; set; } = 0;
        public decimal? NetPrice { get; set; } = 0;
        public Guid? SupplierId { get; set; }
        public string Remark { get; set; } = string.Empty;
        public int RecordId { get; set; }

        public void addSaleBuyTrans(VetSaleBuyTrans trans)
        {
            trans.OwnerId = Id;
            VetSaleBuyTrans.Add(trans);
        }
        public virtual ICollection<VetSaleBuyTrans> VetSaleBuyTrans { get; set; }

    }
}
