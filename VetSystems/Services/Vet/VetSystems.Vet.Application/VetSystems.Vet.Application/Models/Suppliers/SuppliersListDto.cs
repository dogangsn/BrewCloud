using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VetSystems.Vet.Application.Models.Definition.Suppliers
{
    public class SuppliersListDto
    {
        public Guid id{ get; set; }
        public string suppliername { get; set; }
        public string email { get; set; }
        public string phone { get; set; }
        public bool active { get; set; }

    }
}
