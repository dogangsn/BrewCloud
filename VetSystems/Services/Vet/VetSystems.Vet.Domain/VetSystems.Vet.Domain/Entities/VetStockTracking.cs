using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VetSystems.Vet.Domain.Common;

namespace VetSystems.Vet.Domain.Entities
{
    public class VetStockTracking : BaseEntity
    {
        [NotMapped]
        public int RecId { get; set; }
        public Guid ProductId { get; set; }
        public bool Status { get; set; } = true;
        public ProcessTypes ProcessType { get; set; } = ProcessTypes.NewStock;
        public StockTrackingType Type { get; set; }
        public decimal Piece { get; set; } //Adet/Miktar
        public decimal UsedPiece { get; set; }  //Kullanılan
        public decimal RemainingPiece { get; set; } //Kalan
        public Guid? SupplierId { get; set; } //TedarikciId
        public DateTime? ExpirationDate { get; set; } //SonKullanmaTarihi
        public decimal? SalePrice { get; set; } = 0; //SatisFiyati
        public decimal? PurchasePrice { get; set; } = 0; //AlisFiyati
        public Guid? UnitId { get; set; } //Birim
    }


    public enum StockTrackingType
    {
        Entry = 1,
        Exit = 2,
    }

    public enum ProcessTypes
    {
        NewStock = 1, //YeniStock
        Transfer = 2, //Transfer
        Tuning = 3, //Ayarlama
        Other = 4, //Diger
    }
}
