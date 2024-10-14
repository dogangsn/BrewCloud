using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BrewCloud.Account.Domain.Common;

namespace BrewCloud.Account.Domain.Entities
{
    public enum GenderType
    {
        Male,
        Female
    }
    public class Customer : BaseEntity
    {
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Board { get; set; }
        public string Name { get; set; }
        public GenderType Gender { get; set; }
        public string SourceId { get; set; }
        public Guid? ReasonId { get; set; }
        public string RoomNumber { get; set; }
        public int PosSaleType { get; set; }
        public string PosSaleTypeName { get; set; }
        public bool NoPost { get; set; }
        public int? FCardType { get; set; }
        public string EndDate { get; set; }
        public string MemberNumber { get; set; }
        public DateTime? CheckInDate { get; set; }
        public string code { get; set; }
        public Guid? RegionId { get; set; }
        public decimal Limit { get; set; }
        public decimal CLimit { get; set; }
        public string VoucherNo { get; set; }
        public string VoucherRemark { get; set; }
        public DateTime? BirthDate { get; set; }
    }
}
