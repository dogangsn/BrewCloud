using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VetSystems.Account.Domain.Common;
using VetSystems.Account.Domain.Enums;

namespace VetSystems.Account.Domain.Entities
{
    public class Property : BaseEntity
    {
        public Property()
        {
            //Revenuecenters = new HashSet<Revenuecenter>();
            //RevenueCenterIntegrations = new HashSet<RevenueCenterIntegration>();
            //PropertyAccountMaps = new HashSet<PropertyAccountMap>();
            //PropertyDepartmentFinanceMaps = new HashSet<PropertyDepartmentFinanceMap>();
            //PropertyCurrAccountMaps = new HashSet<PropertyCurrAccountMap>();
            //AccommodationMultiplier = new HashSet<AccommodationMultiplier>();
        }

        public string Propertyname { get; set; } = "";
        public string Serveraddress { get; set; } = "";
        public string Timezone { get; set; } = "";
        public string Endoftheday { get; set; } = "";
        public string Calleridrevcenter { get; set; } = "";
        public bool Endofthedayisnextday { get; set; } = false;
        public bool Eftposautoclose { get; set; } = false;
        public bool Propertypayment { get; set; } = false;
        public string Currency { get; set; } = "";
        public string Thousandseperator { get; set; } = "";
        public string Symbolseperator { get; set; } = "";
        public short Symbolposition { get; set; } = 0;
        public short Symbolspacing { get; set; } = 0;
        public string Currencyname { get; set; } = "";
        public string Subcurrencyname { get; set; } = "";
        public string Translationlang { get; set; } = "";
        public string Defaultlang { get; set; } = "";
        public Guid EnterprisesId { get; set; }
        public Guid TimezoneownerId { get; set; }
        public decimal CollectionRatePercentage { get; set; }

        public string Barcode { get; set; }
        public int OrderNumber { get; set; }
        public string DocumentPrefix { get; set; }
        public string DocumentSeri { get; set; }

        public string EInvDocumentPrefix { get; set; }
        public string EInvDocumentSeri { get; set; }

        public bool UseChheckPrintEnterpriseLevel { get; set; }
        public bool UseOrderPrinterEnterpriseLevel { get; set; }
        public EndOfDateType EndOfDateType { get; set; }
        public DateTime? DefaultDate { get; set; }
        public string CompanyTitle { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string MersisNo { get; set; }
        public string WebSite { get; set; }
        public string TaxOffice { get; set; }
        public string TaxNumber { get; set; }


        public string Boards { get; set; }
        public string RoomTypes { get; set; }
        public string Markets { get; set; }
        public Guid? CityLedgerId { get; set; }

        public string Departments { get; set; }
        public Guid? ErpCompanyBranchId { get; set; }
        public Guid? ClAgencyAccountId { get; set; }
        public Guid? FoExtraInvoiceAccountId { get; set; }
        public Guid? FoControlAccountId { get; set; }
        public Guid? ClExtraAccountId { get; set; }
        public virtual Enterprise Enterprises { get; set; }
        [MaxLength(1000)]
        public string SINHotelId { get; set; }
        [MaxLength(250)]
        public string HotelInformationAddress { get; set; }
        [MaxLength(100)]
        public string HotelInformationAppId { get; set; }
        [MaxLength(100)]
        public string HotelInformationAppKey { get; set; }
        [MaxLength(100)]
        public string HotelInformationSecretKey { get; set; }
        //public virtual ICollection<Revenuecenter> Revenuecenters { get; set; }
        //public virtual ICollection<RevenueCenterIntegration> RevenueCenterIntegrations { get; set; }

        //public virtual ICollection<PropertyAccountMap> PropertyAccountMaps { get; set; }
        //public virtual ICollection<PropertyDepartmentFinanceMap> PropertyDepartmentFinanceMaps { get; set; }
        //public virtual ICollection<PropertyCurrAccountMap> PropertyCurrAccountMaps { get; set; }
        //public virtual ICollection<AccommodationMultiplier> AccommodationMultiplier { get; set; }
        public Guid? Regionid { get; set; }
    }
}
