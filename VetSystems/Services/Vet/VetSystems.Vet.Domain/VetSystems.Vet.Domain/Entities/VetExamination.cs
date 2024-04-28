using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VetSystems.Vet.Domain.Common;

namespace VetSystems.Vet.Domain.Entities
{
    public class VetExamination : BaseEntity
    {
        public DateTime Date { get; set; }
        public int Status { get; set; }
        public Guid CustomerId { get; set; }
        public Guid PatientId { get; set; }
        public decimal BodyTemperature { get; set; } = 0; //Vucutısı
        public decimal Pulse { get; set; } = 0; //Nabız
        public decimal RespiratoryRate { get; set; } = 0; //SolunumHizi
        public decimal Weight { get; set; } = 0; //Agirlik
        public Guid SymptomsId { get; set; } //Semptomlar
        public string ComplaintStory { get; set; } = string.Empty; //SikayetHikaye
        public string TreatmentDescription { get; set; } = string.Empty; //TedaviAciklamasi


    }
}
