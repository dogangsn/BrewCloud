using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VetSystems.Vet.Application.Models.Patients.Examinations
{
    public class ExaminationDto
    {
        public Guid Id { get; set; }
        public DateTime Date { get; set; }
        public int Status { get; set; }
        public string CustomerName { get; set; }
        public string PatientName { get; set; }
        //public decimal BodyTemperature { get; set; } = 0; //Vucutısı
        //public decimal Pulse { get; set; } = 0; //Nabız
        //public decimal RespiratoryRate { get; set; } = 0; //SolunumHizi
        public decimal Weight { get; set; } = 0; //Agirlik
        public string Symptoms { get; set; } //Semptomlar
        public string ComplaintStory { get; set; } = string.Empty; //SikayetHikaye
        public string TreatmentDescription { get; set; } = string.Empty; //TedaviAciklamasi

    }
}
